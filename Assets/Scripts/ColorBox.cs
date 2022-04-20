using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorBox : MonoBehaviour
{
    [SerializeField] public GameColors gameColors;

    public GameColor CurrentColor => currentColor;
    private GameColor currentColor;

    void Start()
    {
        currentColor = gameColors.GetRandom();
        gameObject.GetComponentOrNull<MeshRenderer>().material = currentColor.material;
        if (gameObject.GetComponentOrNull<Collider>() == null)
            throw new NullReferenceException();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.AddScores(currentColor, 1);
            Destroy(gameObject);
        }
    }
}