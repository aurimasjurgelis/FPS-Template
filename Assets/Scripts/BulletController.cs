using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed, lifeTime;
    public new Rigidbody rigidbody;
    public GameObject impactEffect;
    public int damage = 1;

    public bool damageEnemy;
    public bool damagePlayer;

    void Start()
    {

    }

    void FixedUpdate()
    {
        rigidbody.velocity = transform.forward * moveSpeed;
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && damageEnemy)
        {
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
        }



        if (other.tag == "Headshot" && damageEnemy)
        {
            other.transform.parent.GetComponent<EnemyHealthController>().DamageEnemy(damage * 2);
            Debug.Log("HEADSHOT");
        }

        if (other.tag == "Player" && damagePlayer)
        {
            Debug.Log("Doing damage to player...");
            PlayerHealthController.instance.DamagePlayer(damage);
        }


        Destroy(this.gameObject);
        Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed) * Time.deltaTime), transform.rotation);
    }
}
