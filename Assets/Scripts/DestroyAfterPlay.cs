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
    private bool isPlaying;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isPlaying)
        {
            if (!audioEvent.audioSource.isPlaying) Destroy(gameObject);
            return;
        }

        if (timer < startDelay)
        {
            timer += Time.deltaTime;
            return;
        }

        audioEvent.Play(audioSource);
        isPlaying = true;
    }
}