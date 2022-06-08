using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    public GameObject item;
    public GameObject text;
    public bool hasCollided = false;
    public bool hasPickedUp = false;

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

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && hasCollided)
        {
            text.SetActive(false);
            GlobalData.TutorialKeys = true;
            item.SetActive(false);
        }
        if(GlobalData.TutorialKeys == true)
        {
            hasPickedUp = true;
        }
    }
}
