using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchArms : MonoBehaviour
{
    public GameObject BowArms;
    public GameObject MeleeArms;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            BowArms.SetActive(false);
            MeleeArms.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            MeleeArms.SetActive(false);
            BowArms.SetActive(true);
        }
    }
}
