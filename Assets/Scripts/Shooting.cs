using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// This is not a functional weapon script. It just shows how to implement shooting and reloading with buttons system.
/// </summary>
public class Shooting : MonoBehaviour
{
    public FP_Input playerInput;

    public float shootRate = 0.15F;
    //public float reloadTime = 1.0F;
    // public int ammoCount = 9999999;

    public GameObject shellPrefab;

    // private int ammo;
    private float timer;
    private bool reloading;
    private Shell shell;

    void Start()
    {
        if (shellPrefab.IsComponentExist<Shell>())
            shell = shellPrefab.GetComponent<Shell>();
        else
            throw new NullReferenceException("Shell component is missing on ShellPrefab.");
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (!playerInput.Shoot()) return; //IF SHOOT BUTTON IS PRESSED (Replace your mouse input)
            Shoot();
            timer = shootRate;
        }

        // if (playerInput.Reload()) //IF RELOAD BUTTON WAS PRESSED (Replace your keyboard input)
        //     if (!reloading && ammoCount < ammo)
        //         StartCoroutine("Reload");
    }
    

    // IEnumerator Reload()
    // {
    //     reloading = true;
    //     Debug.Log("Reloading");
    //     yield return new WaitForSeconds(reloadTime);
    //     ammoCount = ammo;
    //     Debug.Log("Reloading Complete");
    //     reloading = false;
    // }

    void Shoot()
    {
        var newShell = Instantiate(shellPrefab, transform.position, transform.rotation);
        newShell.GetComponentOrNull<MeshRenderer>().material = GameController.Instance.LastColor.material;
    }
}