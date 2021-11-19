using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
    public int unlockedLevel = 4;
    private LevelSelectButton[] levelButons;
    public bool testStarsSystem = true;
    
    private void OnEnable()
    {
        levelButons = GetComponentsInChildren<LevelSelectButton>();
    }

    void Refresh()
    {
        int i = 1;
        foreach (var btn in levelButons)
        {
            btn.gameObject.SetActive(true);
            btn.Setup(i, Random.Range(1, 4), unlockedLevel >= i);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (testStarsSystem)
        {
            Refresh();
            testStarsSystem = false;
        }
    }
}
