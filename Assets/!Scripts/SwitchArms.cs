using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchArms : MonoBehaviour
{
    public GameObject BowArms;
    public GameObject MeleeArms;
    public BowControlLegacy bowTime;

    float countDown;
    // Start is called before the first frame update
    void Start()
    {
        countDown = .5f;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;

        if(bowTime.defaultFov != bowTime.currentFov)
        {
            countDown = .65f;
        }

        if(countDown < 0)
        {
            countDown = 0f;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1) && countDown == 0f)
        {
            BowArms.SetActive(false);
            MeleeArms.SetActive(true);
            //countDown = timeToSwitch;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && countDown == 0f)
        {
            MeleeArms.SetActive(false);
            BowArms.SetActive(true);
            //countDown = timeToSwitch;
        }
    }
}
