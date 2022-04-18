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
    public float reloadTime = 1.0F;
    public int ammoCount = 9999999;

    public GameObject shellPrefab;
    public Material[] shellMaterials;
    
    private int ammo;
    private float delay;
    private bool reloading;
    private Shell shell;

    void Start()
    {
        ammo = ammoCount;
        
        if (shellPrefab.IsComponentExist<Shell>())
            shell = shellPrefab.GetComponent<Shell>();
        else 
            throw new NullReferenceException("Shell component is missing on ShellPrefab.");
        if (shellMaterials.Length < Enum.GetNames(typeof(GameData.ShellColors)).Length)
             throw new ArgumentException("Not enough color materials on Shooting script");
    }

    void Update()
    {
        if (playerInput.Shoot()) //IF SHOOT BUTTON IS PRESSED (Replace your mouse input)
            if (Time.time > delay)
                ShootComand();

        if (playerInput.Reload()) //IF RELOAD BUTTON WAS PRESSED (Replace your keyboard input)
            if (!reloading && ammoCount < ammo)
                StartCoroutine("Reload");
    }

    void ShootComand()
    {
        if (ammoCount > 0)
        {
            Shoot();
            ammoCount--;
        }
        else
            Debug.Log("Empty");

        delay = Time.time + shootRate;
    }

    IEnumerator Reload()
    {
        reloading = true;
        Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadTime);
        ammoCount = ammo;
        Debug.Log("Reloading Complete");
        reloading = false;
    }

    void OnGUI()
    {
        GUILayout.Label("AMMO: " + ammoCount);
    }

    void Shoot()
    {
        var newShell = Instantiate(shellPrefab, transform.position, transform.rotation);
        //TODO add color material to shell
    }
}