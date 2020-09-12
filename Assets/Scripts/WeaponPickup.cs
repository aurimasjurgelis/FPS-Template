using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string gunName;
    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !collected)
        {
            PlayerController.instance.AddGun(gunName);
            Destroy(gameObject);
            collected = true;
            AudioManager.instance.PlaySFX("pickup_gun");
        }
    }
}
