using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] public GameController gameController;
    [SerializeField] public GameObject spawnObject;
    [SerializeField]public float spawnTime = 3f;
    [SerializeField] public float spawnRange = 10f;
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
        {
            timer = 0;
            Spawn();
        }
    }

    private void FixedUpdate()
    {
        if (isPaused || isSpawnPrepared) return;
        var x = Random.value * spawnRange - spawnRange/2;
        var z = Random.value * spawnRange - spawnRange/2;
        var y = 90f;
        
        Vector3 rayStartPos = new Vector3(x,y,z);
        
        RaycastHit hit;
        if (Physics.Raycast(rayStartPos, Vector3.down, out hit,100))
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
        isSpawnPrepared = false;
    }
}
