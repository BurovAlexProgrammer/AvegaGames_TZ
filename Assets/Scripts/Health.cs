using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int initialHealth = 100;
    public Destrictable destrictable;
    public int HealthValue { get; private set; }

    private void Start()
    {
        HealthValue = initialHealth;
        destrictable = gameObject.GetComponentOrNull<Destrictable>();
    }

    public void TakeDamage(int damage)
    {
        HealthValue -= damage;
        if (HealthValue <= 0)
        {
            if (destrictable != null)
                destrictable.Distruct();
        }
    }

    public void Restore()
    {
        HealthValue = initialHealth;
    }

}
