using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrictable : MonoBehaviour
{
    public AnimationClip animationClip;
    public bool canBeDestroyed = false;

    void Destroy()
    {
        GameObject.Destroy(gameObject);
    }

    public void RunDestruction()
    {
        
    }
}
