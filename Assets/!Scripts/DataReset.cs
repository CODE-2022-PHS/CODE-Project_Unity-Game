using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobalData.currHealth = 100f;
        GlobalData.currArrows = 6;
        GlobalData.maxArrows = 6;
    }
}
