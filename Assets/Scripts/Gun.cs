﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public bool canAutoFire;
    public float fireRate;
    [HideInInspector]
    public float fireCounter;

    public int currentAmmo;
    public int pickupAmount;
    public Transform firePoint;

    public float zoomAmount;

    public string gunName;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }
    }

    public void GetAmmo()
    {
        currentAmmo += pickupAmount;
        UIController.instance.ammoText.text = "Ammo: " + currentAmmo;

    }
}
