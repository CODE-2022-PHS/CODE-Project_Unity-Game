using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMessage : MonoBehaviour
{
    public GameObject DeathText;
    public GameObject EscapeText;

    // Update is called once per frame
    void Update()
    {
        if(GlobalData.hasDied == true)
        {
            DeathText.SetActive(true);
        }
        if(GlobalData.hasWon == true)
        {
            EscapeText.SetActive(true);
        }
    }
}
