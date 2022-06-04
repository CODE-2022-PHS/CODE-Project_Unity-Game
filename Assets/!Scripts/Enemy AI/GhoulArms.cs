using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulArms : MonoBehaviour
{

    public float armDamage;
    public GhoulAI enemy;
    public GameObject reference;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(enemy.isAttacking)
            {
                other.GetComponent<PlayerHealth>().Damage(2.5f);
            }
        }
        else if(other.tag != "Player")
        {
            //GetComponent<GhoulAI>().ChasePlayer();
            reference.GetComponent<GhoulAI>().agent.SetDestination(transform.position);
        }
    }
}
