using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchArms : MonoBehaviour
{
    public GameObject BowArms;
    public GameObject MeleeArms;
    public GameObject Crosshair;
    public GameObject BowCrosshair;
    public GameObject SwordImage;
    public GameObject BowImage;


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
            BowCrosshair.SetActive(false);
            BowImage.SetActive(false);

            MeleeArms.SetActive(true);
            Crosshair.SetActive(true);
            SwordImage.SetActive(true);

            countDown = .5f;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && countDown == 0f)
        {
            MeleeArms.SetActive(false);
            Crosshair.SetActive(false);
            SwordImage.SetActive(false);

            BowArms.SetActive(true);
            BowCrosshair.SetActive(true);
            BowImage.SetActive(true);

            countDown = .5f;
        }
    }
}
