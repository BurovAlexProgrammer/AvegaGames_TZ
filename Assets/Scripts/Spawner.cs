using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] public GameObject spawnObject;
    [SerializeField] public float spawnTime = 3f;
    [SerializeField] public float spawnRange = 10f;

    [SerializeField] public float minSpawnDistance = 1.5f;
    public bool isPaused = false;

    private float timer;
    private bool isSpawnPrepared;
    private Vector3 spawnPosition;

    public void StartSpawn()
    {
        isPaused = false;
    }

    public void StopSpawn()
    {
        isPaused = true;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTime)
            PrepareSpawn();
    }

    private void FixedUpdate()
    {
        if (isPaused) return;

        if (isSpawnPrepared)
            Spawn();
    }

    void PrepareSpawn()
    {
        RaycastHit hit;
        var randomOffset = Random.value * spawnRange * 2 - spawnRange;
        var playerPosition = GameController.Instance.playerGO.transform.position;
        var playerRotation = GameController.Instance.playerGO.transform.rotation;
        var rayStartPosition = playerPosition + playerRotation * Vector3.back * minSpawnDistance;
        var rayEndPosition = rayStartPosition + playerRotation * Vector3.back * spawnRange / 2 +
                             Vector3.right * randomOffset;
        var rayDirection = rayEndPosition - rayStartPosition;

#if UNITY_EDITOR

        Debug.DrawLine(rayStartPosition, rayEndPosition, Color.yellow, 1f);
#endif

        if (Physics.Raycast(rayStartPosition, rayDirection, out hit, spawnRange))
        {
            isSpawnPrepared = true;
            spawnPosition = hit.point + Vector3.up * -0.5f;
        }
    }

    void Spawn()
    {
        Vector3 rotationVector = GameController.Instance.playerGO.transform.position - spawnPosition;
        Quaternion spawnRotation = Quaternion.LookRotation(rotationVector, Vector3.up);
        var newEnemy = Instantiate(spawnObject, spawnPosition + spawnRotation * Vector3.forward, spawnRotation);
        newEnemy.GetComponent<Enemy>().target = GameController.Instance.playerGO;

        NavMeshHit closestHit;
        
        if (NavMesh.SamplePosition(spawnPosition + spawnRotation * Vector3.forward, out closestHit, 10, NavMesh.AllAreas))
        {
            newEnemy.transform.position = closestHit.position;
            newEnemy.tag = "Enemy";
            timer = 0;
            isSpawnPrepared = false;
        }
    }
}