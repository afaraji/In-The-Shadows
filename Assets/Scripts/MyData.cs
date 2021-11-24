using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MyData 
{
    public static bool isModeNormal = true;
    public static int currentLvL = 2;
    public static float rotationSpeed
    {
        get{return rotationSpeed * 1600 + 200;}
        set{rotationSpeed = (value - 200)/1600;}
    }
    public static float moveSpeed
    {
        get{return moveSpeed * 80 + 80;}
        set{moveSpeed = value / 80 + 1;}
    }

   
    
    public static void SetLvlStars(int lvl, int stars)
    {
        PlayerPrefs.SetInt("Level_" + lvl, stars);
    }
}
