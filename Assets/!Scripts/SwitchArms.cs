using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchArms : MonoBehaviour
{
    public GameObject BowArms;
    public GameObject MeleeArms;
    public float timeToSwitch = 180;
    float countDown;
    // Start is called before the first frame update
    void Start()
    {
        countDown = timeToSwitch;
    }

    // Update is called once per frame
    void Update()
    {
        countDown--;

        if(countDown < 0)
        {
            countDown = 0;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1) && countDown == 0)
        {
            BowArms.SetActive(false);
            MeleeArms.SetActive(true);
            countDown = timeToSwitch;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && countDown == 0)
        {
            MeleeArms.SetActive(false);
            BowArms.SetActive(true);
            countDown = timeToSwitch;
        }
    }
}
