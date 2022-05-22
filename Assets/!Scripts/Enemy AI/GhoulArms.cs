using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulArms : MonoBehaviour
{

    public float armDamage;
    public GhoulAI enemy;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(enemy.isAttacking)
            {
                other.GetComponent<PlayerHealth>().Damage(2.5f);
            }
        }
    }


    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
