using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIArrow : MonoBehaviour
{
    public Text arrowNum;

    public int arrows;
    public int maxArrows = 6;
    public bool canUseBow;

    // Start is called before the first frame update
    void Start()
    {
        arrows = maxArrows;
        canUseBow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(arrows < 10)
        {
            arrowNum.text = "0" + arrows;
        }
        else
        {
            arrowNum.text = "" + arrows;
        }

        if(arrows == 0)
        {
            canUseBow = false;
        }
        else if(arrows > 0)
        {
            canUseBow = true;
        }
        
    }
}
