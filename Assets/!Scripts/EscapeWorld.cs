using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeWorld : MonoBehaviour
{
    public bool hasCollided = false;
    public GameObject EscapeText;

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

    private void Update()
    {
        if(hasCollided && Input.GetKeyDown(KeyCode.F))
        {
            EscapeText.SetActive(false);
            GlobalData.hasWon = true;
            SceneManager.LoadScene("GameOver");
        }
    }
}
