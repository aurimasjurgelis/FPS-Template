using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage = 25;
    public bool damageEnemy;
    public bool damagePlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && damageEnemy)
        {
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
        }

        if (other.tag == "Player" && damagePlayer)
        {
            Debug.Log("Doing damage to player...");
            PlayerHealthController.instance.DamagePlayer(damage);
        }
    }
}
