using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemToDrop : MonoBehaviour
{
    [SerializeField] public GameObject item;

    private bool isAppQuitting;

    private void OnApplicationQuit()
    {
        isAppQuitting = true;
    }

    private void OnDestroy()
    {
        if (isAppQuitting) return;
        if (item == null)
            throw new NullReferenceException();

        var newItem = Instantiate(item, gameObject.transform.position, gameObject.transform.rotation);

        if (newItem.IsComponentExist<Rigidbody>())
        {
            var rigid = newItem.GetComponent<Rigidbody>();
            rigid.velocity = Vector3.up * 8f * Random.value + Vector3.back * Random.value +
                             Vector3.left * Random.value;
            rigid.angularVelocity = new Vector3(0, Random.Range(-90, 90), Random.Range(-30, 30));
        }
    }
}