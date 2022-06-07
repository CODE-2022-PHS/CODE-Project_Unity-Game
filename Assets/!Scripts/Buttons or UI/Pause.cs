using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject slider;
    public Text sensitivityText;


    public bool isPaused = false;


    // Update is called once per frame
    void Update()
    {
        sensitivityText.text = GlobalData.sensitivity.ToString("f2");
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseMenu.SetActive(true);
            slider.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        slider.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
