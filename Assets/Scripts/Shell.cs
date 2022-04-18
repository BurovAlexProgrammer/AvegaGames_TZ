using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shell : MonoBehaviour
{
    public float initialSpeed = 100;
    public float lifeTime = 5;

    private Rigidbody rigidbody;
    private bool isLive = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Live();
        rigidbody.velocity = transform.rotation * Vector3.forward * initialSpeed;
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
        isLive = false;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SendMessage("TakeDamage", SendMessageOptions.RequireReceiver);
        }

        Destroy();
    }
}