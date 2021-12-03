using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MyData 
{
    public static bool isModeNormal = true;
    public static int currentLvL = 1;
    public static bool isGamePaused = false;
    public static float rotationSpeed = 1000f;
    public static float moveSpeed = 100f;
    public static int lastLevelUnlocked;

   
    
    public static void SetLvlStars(int lvl, int stars)
    {
        PlayerPrefs.SetInt("Level_" + lvl, stars);
    }
}
