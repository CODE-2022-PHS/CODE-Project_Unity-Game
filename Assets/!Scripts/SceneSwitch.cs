using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] KeyCode interactKey;
    [SerializeField] bool hasCollided = false;
    [SerializeField] string textLabel = "Hold F to exit level";
    
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

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }
    */
    // Update is called once per frame
    
    void Update()
    {
        if(Input.GetKeyDown(interactKey) && hasCollided)
        {
            SceneManager.LoadScene("OpenWorld1");
        }
    }
    
}
