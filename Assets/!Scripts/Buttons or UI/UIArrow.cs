using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIArrow : MonoBehaviour
{
    public Text arrowNum;

    public int arrows;
    public int maxArrows;
    public bool canUseBow;

    //bool hasUpgraded = false;

    // Start is called before the first frame update
    void Start()
    {
        //arrows = maxArrows;
        canUseBow = true;
        //maxArrows = GlobalData.maxArrows;
    }

    // Update is called once per frame
    void Update()
    {
        arrows = GlobalData.currArrows;
        maxArrows = GlobalData.maxArrows;
        if(arrows > maxArrows)
        {
            GlobalData.currArrows = maxArrows;
        }

        if(GlobalData.QuiverUpgrade == true)
        {
            GlobalData.maxArrows += 3;
            GlobalData.currArrows += 3;
            GlobalData.QuiverUpgrade = false;
        }

        if(arrows < 10)
        {
            arrowNum.text = "0" + GlobalData.currArrows;
        }
        else
        {
            arrowNum.text = "" + GlobalData.currArrows;
        }

        if(GlobalData.currArrows == 0)
        {
            canUseBow = false;
        }
        else if(GlobalData.currArrows > 0)
        {
            canUseBow = true;
        }
        
    }
}
