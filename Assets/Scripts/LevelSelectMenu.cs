using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
    private LevelSelectButton[] levelButons;
    
    private void OnEnable()
    {
        levelButons = GetComponentsInChildren<LevelSelectButton>();
    }

    void Refresh(int unlockedLevel, int maxLvl)
    {
        int i = 1;
        foreach (var btn in levelButons)
        {
            btn.gameObject.SetActive(true);
            btn.Setup(i, Random.Range(1, 4), unlockedLevel >= i);
            i++;
        }
    }
}
