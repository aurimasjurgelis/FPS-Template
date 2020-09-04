using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private bool chasing;
    public float distanceToChase = 10f;
    public float distanceToLose = 15f;
    public float distanceToStop = 2f;

    private Vector3 targetPoint;
    private Vector3 startPoint;


    public NavMeshAgent agent;

    public float chasingTime = 5f;
    private float chaseCounter;

    public GameObject bullet;
    public Transform firePoint;

    public float fireRate, waitBetweenShots = 2f, timeToShoot;
    private float fireCount, shotWaitCounter, shootTimeCounter;

    public Animator anim;

    void Start()
    {
        startPoint = transform.position;
        shootTimeCounter = timeToShoot;
        shotWaitCounter = waitBetweenShots;
    }

    void Update()
    {
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;

        if (!chasing)
        {
            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chasing = true;
                shootTimeCounter = timeToShoot;
                shotWaitCounter = waitBetweenShots;
            }
            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;
                if (chaseCounter <= 0)
                {
                    agent.destination = startPoint;
                }
            }
            if (agent.remainingDistance < .25f)
            {
                anim.SetBool("isMoving", false);
            }
            else
            {
                anim.SetBool("isMoving", true);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
            {
                agent.destination = targetPoint;
            }
            else
            {
                agent.destination = transform.position;
            }

            if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                chasing = false;
                chaseCounter = chasingTime;
            }

            if (shotWaitCounter > 0)
            {
                shotWaitCounter -= Time.deltaTime;
                if (shotWaitCounter <= 0)
                {
                    shootTimeCounter = timeToShoot;
                }
                anim.SetBool("isMoving", true);
            }
            else
            {
                if (PlayerController.instance.gameObject.activeInHierarchy)
                {


                    shootTimeCounter -= Time.deltaTime;


                    if (shootTimeCounter > 0)
                    {
                        fireCount -= Time.deltaTime;
                        if (fireCount <= 0)
                        {
                            fireCount = fireRate;

                            firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.5f, 0f));

                            //check the angle to the player
                            Vector3 targetDirection = PlayerController.instance.transform.position - transform.position;
                            float angle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);

                            if (Mathf.Abs(angle) < 30f)
                            {
                                Instantiate(bullet, firePoint.position, firePoint.rotation);
                                anim.SetTrigger("fireShot");
                            }
                            else
                            {
                                shotWaitCounter = waitBetweenShots;
                            }

                        }
                        agent.destination = transform.position;
                    }
                    else
                    {
                        shotWaitCounter = waitBetweenShots;
                    }
                }
                anim.SetBool("isMoving", false);
            }
        }
    }
}
