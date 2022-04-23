using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DestroyAfterPlay : MonoBehaviour
{
    [SerializeField] public AudioEvent audioEvent;
    [SerializeField] public float startDelay;

    private float timer;
    private bool isReadyToPlay;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (timer < startDelay)
        {
            timer += Time.deltaTime;
            return;
        }

        if (!isReadyToPlay)
        {
            audioEvent.Play(audioSource);
        }
        
        isReadyToPlay = true;
    }
    
}
