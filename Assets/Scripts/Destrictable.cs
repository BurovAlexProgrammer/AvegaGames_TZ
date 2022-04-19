using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrictable : MonoBehaviour
{
    [SerializeField] public GameObject distructPerfab;
    //private bool mustBeDestroyed = false;

    public void Distruct()
    {
        DestructProcess();
    }

    void DestructProcess()
    {
        if (distructPerfab != null)
        {
            var distructGO = Instantiate(distructPerfab, transform.position, transform.rotation, transform.parent);
            distructGO.transform.localScale = distructPerfab.transform.localScale;
        }

        Destroy(gameObject);
    }
}