using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemies : MonoBehaviour
{
    public GameObject Enemies;
    public bool hasCollided = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            hasCollided = true;
        }
    }

    private void Update()
    {
        if(hasCollided == true)
        {
            Enemies.SetActive(true);
        }
    }
}
