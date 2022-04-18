using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public GameObject playerGO;

    public int playerHealthValue => playerHealth.HealthValue;
    public bool IsGameOver => isGameOver;

    private Health playerHealth;
    private int[] scores;
    private bool isGameOver = false;
    private Vector3 initialPlayerPosition;


    void Start()
    {
        var colorLength = Enum.GetNames(typeof(GameData.ShellColors)).Length;
        scores = new int[colorLength];
        initialPlayerPosition = playerGO.transform.position;
        playerHealth = playerGO.GetComponentOrNull<Health>();
    }

    public int GetScores(GameData.ShellColors color)
    {
        return scores[(int) color];
    }

    private void Update()
    {
        if (!isGameOver & playerHealth.HealthValue <= 0)
            GameOver();
        //temp
        // if (Input.anyKeyDown)
        //     playerHealth.TakeDamage(30);
    }

    private async void GameOver()
    {
        Debug.Log("GameOver");
        var timer = 0f;
        var gameOverDelay = 1.5f;
        isGameOver = true;
        while (isGameOver)
        {
            await Task.Yield();
            timer += Time.deltaTime;
            if (timer > gameOverDelay)
            {
                isGameOver = false;
                Restart();
            }
        }
    }

    private void Restart()
    {
        //TODO GameOver and scene reset
        playerGO.transform.SetPositionAndRotation(initialPlayerPosition, Quaternion.identity);
        playerGO.GetComponent<FP_CameraLook>().PlayerHead.rotation = Quaternion.identity;
        playerHealth.Restore();
        Start();
    }
}