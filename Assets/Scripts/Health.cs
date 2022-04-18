using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int initialHealth = 100;
    public Destrictable destrictable;

    private float health;

    private void Start()
    {
        health = initialHealth;
        destrictable = gameObject.GetComponentOrNull<Destrictable>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (destrictable is null)
                Destroy(gameObject);
            else 
                destrictable.RunDestruction();
        }
    }
    
}
