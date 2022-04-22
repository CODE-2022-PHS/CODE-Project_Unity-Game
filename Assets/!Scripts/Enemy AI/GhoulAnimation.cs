using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    //[SerializeField] string idle;
    [SerializeField] string walk;
    [SerializeField] string run;
    [SerializeField] string attack;
    //[SerializeField] string attack2;
    [SerializeField] string death;

    //[SerializeField] float transition = 0.5f; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*float initialTransition = transition;
        if(transition == 0)
        {
            transition = initialTransition;
        }*/

        if(GetComponent<GhoulAI>().playerInSightRange && GetComponent<GhoulAI>().playerInAttackRange)
        {
            //transition--;
            GetComponent<Animation>().Play(attack);
        }
        if(GetComponent<GhoulAI>().playerInSightRange && !GetComponent<GhoulAI>().playerInAttackRange)
        {
            //transition--;
            GetComponent<Animation>().Play(run);
        }
        if(!GetComponent<GhoulAI>().playerInSightRange && !GetComponent<GhoulAI>().playerInAttackRange)
        {
            //transition--;
            GetComponent<Animation>().Play(walk);
        }
        if(GetComponent<GhoulAI>().health == 0)
        {
            GetComponent<Animation>().Play(death);
        }
    }
}
