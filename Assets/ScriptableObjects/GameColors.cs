using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Custom/GameColors")]
public class GameColors : ScriptableObject
{
    [SerializeField] public GameColor[] materials;

    public GameColor GetRandom()
    {
        var colorIndex = Random.Range(0, materials.Length - 1);
        return materials[colorIndex];
    }
}

[Serializable]
public class GameColor
{
    public string name;
    public Material material;
    public Color color;
}