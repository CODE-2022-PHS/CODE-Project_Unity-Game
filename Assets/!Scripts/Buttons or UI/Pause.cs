using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject sliderObject;
    public GameObject requirementsText;
    public GameObject line;
    public Text sensitivityText;
    public Slider sensitivitySlider;


    public bool isPaused = false;
    bool lineActive = false;

    private void Start()
    {
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity");
    }

    // Update is called once per frame
    void Update()
    {
        sensitivityText.text = GlobalData.sensitivity.ToString("f2");
        PlayerPrefs.SetFloat("sensitivity", GlobalData.sensitivity);

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseMenu.SetActive(true);
            sliderObject.SetActive(true);
            requirementsText.SetActive(true);

            if(GlobalData.TutorialKeys == true || GlobalData.foundMap == true)
            {
                line.SetActive(true);
                lineActive = true;
            }

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        sliderObject.SetActive(false);
        requirementsText.SetActive(false);

        if(lineActive == true)
        {
            line.SetActive(false);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
