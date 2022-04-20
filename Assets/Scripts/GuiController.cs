using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GuiController : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI health;
    [SerializeField] public TextMeshProUGUI redScores;
    [SerializeField] public TextMeshProUGUI yellowScores;
    [SerializeField] public TextMeshProUGUI greenScores;
    [SerializeField] public TextMeshProUGUI gameOver;

    private GameColor[] colors;
    void Start()
    {
        colors = GameController.Instance.gameColors.colors;
    }
    
    void Update()
    {
        health.text = $"HP: {GameController.Instance.playerHealthValue.ToString()}";
        redScores.text = GameController.Instance.GetScores(colors[0].name).ToString();
        yellowScores.text = GameController.Instance.GetScores(colors[1].name).ToString();
        greenScores.text = GameController.Instance.GetScores(colors[2].name).ToString();
        gameOver.gameObject.SetActive(GameController.Instance.IsGameOver);
    }
    
    
}
