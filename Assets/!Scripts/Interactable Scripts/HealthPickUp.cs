using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public GameObject item;
    public GameObject text;
    public GameObject NoticeText;
    public bool hasCollided = false;
    public bool hasPickedUp = false;
    float countDown;
    bool startCountdown = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hasCollided = true;
            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            hasCollided = false;
            text.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        countDown = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && hasCollided && GlobalData.currHealth < 100)
        {
            text.SetActive(false);
            GlobalData.aidKitFound = true;
            item.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.F) && hasCollided && GlobalData.currHealth == 100)
        {
            NoticeText.SetActive(true);
            startCountdown = true;
        }
        if (GlobalData.aidKitFound == true)
        {
            hasPickedUp = true;
            GlobalData.currHealth += 25;
            GlobalData.aidKitFound = false;
        }

        if (startCountdown == true)
        {
            countDown -= Time.deltaTime;
        }
        if (countDown <= 0)
        {
            startCountdown = false;
            countDown = 5f;
            NoticeText.SetActive(false);
        }
    }
}
