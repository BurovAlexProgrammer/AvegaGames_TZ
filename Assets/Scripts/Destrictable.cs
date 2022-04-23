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
            var parentRigid = gameObject.GetComponentOrNull<Rigidbody>();
            var distructGO = Instantiate(distructPerfab, transform.position, transform.rotation, transform.parent);
            distructGO.transform.localScale = distructPerfab.transform.localScale;
            distructGO.transform.rotation = distructPerfab.transform.rotation;

            if (parentRigid != null)
            {
                foreach (Transform child in distructGO.transform)
                {
                    var rigid = child.gameObject.GetComponentOrNull<Rigidbody>();
                    rigid.velocity = parentRigid.velocity;
                    rigid.angularVelocity = parentRigid.angularVelocity;
                }
            }
        }

        Destroy(gameObject);
    }
}