using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensitivitySlider : MonoBehaviour
{

    public GameObject Player;

    public void Sensitivity(float value)
    {
        GlobalData.sensitivity = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
