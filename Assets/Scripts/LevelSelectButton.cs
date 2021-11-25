using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    public GameObject levelStarsPrefab;
    private LevelStars levelstars;

    private void OnEnable()
    {
        levelstars = Instantiate(levelStarsPrefab, transform).GetComponent<LevelStars>();
    }

    public void Setup(int level, int stars, bool isUnlocked)
    {
        
        levelstars.gameObject.SetActive(true);
        if (!isUnlocked)
        {
            GetComponent<Button>().interactable = false;
            levelstars.SetStars(0);
            //Debug.Log($"lvelselectbutton:({level}) is not open ---");
        }
        else
        {
            levelstars.SetStars(stars);
            var btn = GetComponent<Button>();
            btn.interactable = true;
            //Debug.Log($"lvelselectbutton:({level}) is open +++ {btn.IsInteractable()} | {btn.isActiveAndEnabled}");
        }
    }
}
