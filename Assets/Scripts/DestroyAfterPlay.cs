using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        if (isReadyToPlay) return;

        if (timer < startDelay)
        {
            timer += Time.deltaTime;
            return;
        }

        if (!isReadyToPlay)
        {
            audioEvent.Play(audioSource);
            DestroyOnPlayEnd();
        }

        isReadyToPlay = true;
    }

    async void DestroyOnPlayEnd()
    {
        while (this != null && audioSource != null && audioEvent.audioSource.isPlaying)
        {
            await Task.Yield();
        }

        if (this != null) Destroy(this);
    }
}