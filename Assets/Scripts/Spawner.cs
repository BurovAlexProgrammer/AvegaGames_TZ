using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] public GameController gameController;
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
        var playerPosition = gameController.playerGO.transform.position;
        var playerRotation = gameController.playerGO.transform.rotation;
        var rayStartPosition = playerPosition + playerRotation * Vector3.back * minSpawnDistance;
        var rayEndPosition = rayStartPosition + playerRotation * Vector3.back * spawnRange /2 + Vector3.right * randomOffset;
        var rayDirection = rayEndPosition - rayStartPosition;

#if UNITY_EDITOR
        
        Debug.DrawLine(rayStartPosition, rayEndPosition, Color.yellow, 1f);
#endif

        if (Physics.Raycast(rayStartPosition, rayDirection, out hit, spawnRange))
        {
            isSpawnPrepared = true;
            spawnPosition = hit.point + Vector3.up * 0.1f;
        }
    }

    void Spawn()
    {
        Vector3 rotationVector = gameController.playerGO.transform.position - spawnPosition;
        Quaternion spawnRotation = Quaternion.LookRotation(rotationVector, Vector3.up);
        var newEnemy = Instantiate(spawnObject, spawnPosition, spawnRotation);
        timer = 0;
        isSpawnPrepared = false;
    }
}