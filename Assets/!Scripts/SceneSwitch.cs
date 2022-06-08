using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] KeyCode interactKey;
    [SerializeField] bool hasCollided = false;
    [SerializeField] string textLabel = "Hold F to exit level";
    public GameObject ExitNotice;

    public float countDown;
    bool startCountdown = false;

    void OnGUI()
        {
            if(hasCollided == true)
            {
                GUI.Box(new Rect(50, Screen.height - 50, Screen.width - 150, 120), (textLabel));
            }
        }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            hasCollided = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            hasCollided = false;
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
        if(Input.GetKeyDown(interactKey) && hasCollided)
        {
            if(GlobalData.TutorialKeys == true)
            {
                GlobalData.TutorialKeys = false;
                SceneManager.LoadScene("OpenWorld1");
            }
            else if(GlobalData.TutorialKeys == false)
            {

                startCountdown = true;
                ExitNotice.SetActive(true);
            }
            
        }

        if(startCountdown == true)
        {
            countDown -= Time.deltaTime;
        }

        if(countDown <= 0)
        {
            startCountdown = false;
            countDown = 5f;
            ExitNotice.SetActive(false);
        }
    }
    
}
