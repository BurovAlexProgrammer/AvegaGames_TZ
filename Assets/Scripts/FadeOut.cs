using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField] public float timeToFade = 0.5f;
    [SerializeField] public float scaleFactor = 1f;

    private float initTime;
    private Vector3 initScale;

    void Start()
    {
        initTime = timeToFade;
        initScale = transform.localScale;
    }

    void Update()
    {
        if (timeToFade > 0)
        {
            timeToFade -= Time.deltaTime;
            scaleFactor = Mathf.Clamp01(timeToFade / initTime);
            gameObject.transform.localScale = initScale * scaleFactor;
            return;
        }
        
        Destroy(gameObject);
    }
}
