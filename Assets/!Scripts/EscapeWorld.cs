using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeWorld : MonoBehaviour
{
    public bool hasCollided = false;
    public GameObject EscapeText;
    public GameObject ExitNotice;
    public float countDown;
    bool startCountdown = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            hasCollided = true;
            EscapeText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            hasCollided = false;
            EscapeText.SetActive(false);
        }
    }

    private void Start()
    {
        countDown = 5f;
    }

    private void Update()
    {
        if(hasCollided && Input.GetKeyDown(KeyCode.F))
        {
            if(GlobalData.safetyKitFound == true && GlobalData.foundMap == true)
            {
                EscapeText.SetActive(false);
                GlobalData.hasWon = true;
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                startCountdown = true;
                ExitNotice.SetActive(true);
            }
        }

        if (startCountdown == true)
        {
            countDown -= Time.deltaTime;
        }

        if (countDown <= 0)
        {
            startCountdown = false;
            countDown = 5f;
            ExitNotice.SetActive(false);
        }
    }
}
