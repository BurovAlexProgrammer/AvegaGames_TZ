using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GuiController : MonoBehaviour
{
    [SerializeField] public GameController gameController;
    [SerializeField] public TextMeshProUGUI health;
    [SerializeField] public TextMeshProUGUI redScores;
    [SerializeField] public TextMeshProUGUI yellowScores;
    [SerializeField] public TextMeshProUGUI greenScores;
    [SerializeField] public TextMeshProUGUI gameOver;
    void Start()
    {
        
    }
    
    void Update()
    {
        health.text = $"HP: {gameController.playerHealthValue.ToString()}";
        redScores.text = gameController.GetScores(GameData.ShellColors.red).ToString();
        yellowScores.text = gameController.GetScores(GameData.ShellColors.yellow).ToString();
        greenScores.text = gameController.GetScores(GameData.ShellColors.green).ToString();
        gameOver.gameObject.SetActive(gameController.IsGameOver);
    }
    
    
}
