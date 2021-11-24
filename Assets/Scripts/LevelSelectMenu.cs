using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    private LevelSelectButton[] levelButons;
    
    private void OnEnable()
    {
        levelButons = GetComponentsInChildren<LevelSelectButton>();
    }

    public void RefreshButtons(int unlockedLevel, int maxLvl)
    {
        int i = 1;
        foreach (var btn in levelButons)
        {
            if (i <= maxLvl)
            {
                btn.gameObject.SetActive(true);
                btn.Setup(i, GetLvlStars(i), unlockedLevel >= i);
            }
            else
            {
                btn.gameObject.SetActive(false);
            }
            i++;
        }
    }
    
    int GetLvlStars(int lvl)
    {
        return (PlayerPrefs.GetInt("Level_" + lvl, 0));
    }

    public void OnLvlButtonPressed(int lvlIndex)
    {
        Debug.Log("Loading level: " + lvlIndex);
        SceneManager.LoadScene("Level_" + lvlIndex);
    }
}

/*
 * ************  PlayerPrefs content *************
 * GeneralVolume
 * SFX
 * Music
 * RotationSpeed
 * UnlockedLvl
 * TotalLvls
 * isNormalMode
 */