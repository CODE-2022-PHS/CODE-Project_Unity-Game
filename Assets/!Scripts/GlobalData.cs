using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalData : MonoBehaviour
{
    //Health Variable
    public static float currHealth = 100f;

    //Arrow Variables
    public static int currArrows = 6;
    public static int maxArrows = 6;

    //GameOver Message Variables
    public static bool hasDied = false;
    public static bool hasWon = false;

    //Player Settings
    public static float sensitivity = 3.5f; 
}
