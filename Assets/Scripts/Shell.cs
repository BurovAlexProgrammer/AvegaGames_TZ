using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shell : MonoBehaviour
{
    [SerializeField] public float initialSpeed = 100;
    [SerializeField] public float lifeTime = 5;
    [SerializeField] public int damage = 50;
    [SerializeField] public AudioEvent shootAudioEvent;

    private Rigidbody thisRigidbody;
    private bool isLive = true;

    private void OnApplicationQuit()
    {
        DestroyImmediate(this);
    }
    
    void Start()
    {
        gameObject.CreateAudioEvent(shootAudioEvent);
        thisRigidbody = GetComponent<Rigidbody>();
        Live();
        thisRigidbody.velocity = transform.rotation * Vector3.forward * initialSpeed;
    }

    async void Live()
    {
        var timer = 0f;
        while (isLive)
        {
            if (timer >= lifeTime)
                Destroy();
            await Task.Yield();
            timer += Time.deltaTime;
        }
    }

    void Destroy()
    {
        if (this == null) return;
        isLive = false;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemyHealth = other.gameObject.GetComponentOrNull<Health>();

            if (enemyHealth != null)
                enemyHealth.TakeDamage(damage);
            else
                throw new Exception("Health component does not contains on Enemy game object.");
        }

        Destroy();
    }
}