using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public GameObject playerGO;
    [SerializeField] public bool isPlayMusic;
    [SerializeField] public GameObject musicPlayer;
    [SerializeField] public GameColors gameColors;

    private static Dictionary<string, int> Scores = new Dictionary<string, int>();

    private static GameController _instance;
    public static GameController Instance => _instance;

    public int playerHealthValue => playerHealth.HealthValue;
    public bool IsGameOver => isGameOver;

    private Health playerHealth;
    private int[] scores;
    private bool isGameOver = false;
    private Vector3 initialPlayerPosition;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else throw new Exception("GameController Instance is defined already.");

        foreach (var color in gameColors.colors)
        {
            Scores.Add(color.name, 0);
        }
    }

    void Start()
    {
        initialPlayerPosition = playerGO.transform.position;
        playerHealth = playerGO.GetComponentOrNull<Health>();
        musicPlayer.SetActive(isPlayMusic);
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

        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            DestroyImmediate(enemy);
        }

        foreach (var item in GameObject.FindGameObjectsWithTag("CollectItem"))
        {
            DestroyImmediate(item);
        }

        Start();
    }

    public void AddScores(string colorName, int value)
    {
        Scores[colorName] = value;
    }

    public int GetScores(string colorName)
    {
        return Scores[colorName];
    }
}