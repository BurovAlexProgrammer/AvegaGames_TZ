using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellDestroyed : MonoBehaviour
{
    [SerializeField] public Material material;
    [SerializeField] public Vector3 velocity;
    [SerializeField] public Vector3 angularVelocity;

    public ShellDestroyed(Material material, Vector3 velocity, Vector3 angularVelocity)
    {
        this.material = material;
        this.velocity = velocity;
        this.angularVelocity = angularVelocity;
    }

    void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            var childRenderer = child.gameObject.GetComponent<MeshRenderer>();
            childRenderer.material = material;
            var childRigid = child.gameObject.GetComponent<Rigidbody>();
            childRigid.angularVelocity = angularVelocity * 10f;
            childRigid.velocity = velocity / 5f +
                                  (transform.rotation * new Vector3(Random.value, Random.value + 1)) * 10f;
        }
    }

    void FixedUpdate()
    {
        if (gameObject.transform.childCount == 0) Destroy(gameObject);
    }
}