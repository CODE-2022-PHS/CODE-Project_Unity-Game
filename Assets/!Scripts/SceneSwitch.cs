using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] KeyCode interactKey;
    [SerializeField] bool hasCollided = false;
    public GameObject ExitNotice;
    public GameObject canLeaveText;

    public float countDown;
    bool startCountdown = false;


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            hasCollided = true;
            canLeaveText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            hasCollided = false;
            canLeaveText.SetActive(false);
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
                canLeaveText.SetActive(false);
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
