using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDamage : MonoBehaviour
{
    // Start is called before the first frame update

    public float arrowDamage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<GhoulAI>().TakeDamage(arrowDamage);
        }
    }
}
