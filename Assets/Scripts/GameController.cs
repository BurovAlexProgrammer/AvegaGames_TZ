using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance => _instance;

    [SerializeField] public GameObject playerGO;
    [SerializeField] public bool isPlayMusic;
    [SerializeField] public GameObject musicPlayer;
    [SerializeField] public GameColors gameColors;
    [SerializeField] public AudioEvent startGameAudio;
    [SerializeField] public AudioEvent gameOverAudio;

    public GameColor LastColor => lastColor;
    private GameColor lastColor;

    private static Dictionary<string, int> Scores = new Dictionary<string, int>();


    public int playerHealthValue => playerHealth.HealthValue;
    public bool IsGameOver => isGameOver;

    private Health playerHealth;
    private bool isGameOver = false;
    private Vector3 initialPlayerPosition;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else throw new Exception("GameController Instance is defined already.");
        lastColor = gameColors.colors[0];
        InitialScores();
    }

    void Start()
    {
        initialPlayerPosition = playerGO.transform.position;
        playerHealth = playerGO.GetComponentOrNull<Health>();
        playerGO.CreateAudioEvent(startGameAudio);
    }

    private void Update()
    {
        if (!isGameOver & playerHealth.HealthValue <= 0)
            GameOver();
        musicPlayer.SetActive(isPlayMusic);
    }

    private async void GameOver()
    {
        Debug.Log("GameOver");
        playerGO.CreateAudioEvent(gameOverAudio);
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
        playerGO.transform.SetPositionAndRotation(initialPlayerPosition, Quaternion.identity);
        playerGO.GetComponent<FP_CameraLook>().PlayerHead.rotation = Quaternion.identity;
        playerHealth.Restore();
        InitialScores();

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

    public void AddScores(GameColor color, int value)
    {
        lastColor = color;
        Scores[color.name] += value;
    }

    public int GetScores(string colorName)
    {
        return Scores[colorName];
    }

    public void InitialScores()
    {
        Scores.Clear();
        foreach (var color in gameColors.colors)
        {
            Scores.Add(color.name, 0);
        }
    }

    public void SwitchMusic(bool isOn)
    {
        isPlayMusic = isOn;
        GameObject.Find("MusicToggleCross").GetComponentOrNull<TextMeshProUGUI>().SetText(isOn ? "" : "  I");
    }
}