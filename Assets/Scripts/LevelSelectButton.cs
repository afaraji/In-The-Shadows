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
        //DontDestroyOnLoad(clickSound);
        
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
            if (MyData.lastLevelUnlocked == level)
            {
                MyData.lastLevelUnlocked = 0;
                //StartCoroutine(AnimateLevelUnlock());
                StartCoroutine(ColorLevelUnlock());
            }
            //Debug.Log($"lvelselectbutton:({level}) is open +++ {btn.IsInteractable()} | {btn.isActiveAndEnabled}");
        }
    }
    
    IEnumerator ColorLevelUnlock()
    {
        Debug.Log("color coroutine");
        bool musicIsPlaying = AudioManager.audioManager.music.isPlaying;
        AudioManager.audioManager.PlayMusic(2);
        var animator = GetComponent<Animator>();
        var button = GetComponent<Image>();
        var btnTransform = GetComponent<RectTransform>();
        animator.enabled = false;
        var originalColor = button.color;
        float delay = 3f;
        while (delay >= 0)
        {
            button.color = Random.ColorHSV();
            btnTransform.localScale = Vector3.one * (0.8f + Mathf.PingPong(delay, 0.6f));
            delay -= Time.deltaTime;
            yield return new WaitForSeconds(0.006f);
        }
        button.color = originalColor;
        animator.enabled = true;
        if (musicIsPlaying)
            AudioManager.audioManager.PlayMusic(0);
    }

    
}
