using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaySceneManager : MonoBehaviour
{
    public GameObject hintButton;
    public GameObject menuButton;
    public GameObject menuCanvas;
    public GameObject lvlSolvedCanvas;
    public GameObject settingMenuCanvas;
    public GameObject hintCanvas;
    public GameObject nextLevelButton;
    public ModelData[] model;
    public RotationManager[] models;
    public RectTransform imageChangeScale;
    public float difficulty;
    public float totalProgress;
    public Image starsOnSolved;
    public Sprite[] stars;
    
    private float threeStarsTime = 10f;
    private float twoStarsTime = 15f;
    private float startTime;


    void Start()
    {
        menuCanvas.SetActive(false);
        lvlSolvedCanvas.SetActive(false);
        settingMenuCanvas.SetActive(false);
        startTime = Time.time;
    }

    
    private void LateUpdate()
    {
        totalProgress = 0;
        foreach (var m in models)
        {
            totalProgress += m.solutionPorg;
        }
        Debug.Log($"total prgress: {totalProgress} for {models.Length} objects");
        totalProgress /= models.Length;
        imageChangeScale.transform.localScale = new Vector3(1, totalProgress, 1);
        if (totalProgress < difficulty / 100)
        {
            int numOfStars;
            if (Time.time - startTime <= threeStarsTime)//3stars
            {
                starsOnSolved.sprite = stars[2];
                numOfStars = 3;
            }
            else if (Time.time - startTime <= twoStarsTime)//2stars
            {
                starsOnSolved.sprite = stars[1];
                numOfStars = 2;
            }
            else//1star
            {
                starsOnSolved.sprite = stars[0];
                numOfStars = 1;
            }
            OnLevelSolved(numOfStars);
        }
    }

    IEnumerator ShowHint(string txt)
    {
        hintCanvas.SetActive(true);
        hintCanvas.GetComponentInChildren<Text>().text = txt;
        yield return new WaitForSeconds(3);
        hintCanvas.SetActive(false);
    }

    public void hntButtonPressed()
    {
        Debug.Log("show hint for : " + model[0].name);
        StartCoroutine(ShowHint(model[0].hint));
    }

    public void pauseButton()
    {
        menuCanvas.SetActive(true);
        lvlSolvedCanvas.SetActive(false);
        settingMenuCanvas.SetActive(false);
        hintButton.SetActive(false);
        menuButton.SetActive(false);
        MyData.isGamePaused = true;
    }
    
    public void mainMenuButton()
    {
        MyData.isGamePaused = false;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void settingButton()
    {
        menuCanvas.SetActive(false);
        lvlSolvedCanvas.SetActive(false);
        settingMenuCanvas.SetActive(true);
        hintButton.SetActive(false);
        menuButton.SetActive(false);
        MyData.isGamePaused = true;
    }

    public void continueButton()
    {
        menuCanvas.SetActive(false);
        lvlSolvedCanvas.SetActive(false);
        settingMenuCanvas.SetActive(false);
        hintButton.SetActive(true);
        menuButton.SetActive(true);
        MyData.isGamePaused = false;
    }

    public void OnLevelSolved(int numOfStars)
    {
        menuCanvas.SetActive(false);
        lvlSolvedCanvas.SetActive(true);
        settingMenuCanvas.SetActive(false);
        hintButton.SetActive(false);
        menuButton.SetActive(false);
        MyData.isGamePaused = true;

        // if last level disable next lvl button
        int lvls = PlayerPrefs.GetInt("TotalLvls");
        if (lvls == MyData.currentLvL)
            nextLevelButton.SetActive(false);
        else
            PlayerPrefs.SetInt("UnlockedLvl", MyData.currentLvL + 1);
        MyData.SetLvlStars(MyData.currentLvL, numOfStars);
        // update playerprefs

    }

    public void OnBackFromSettingButton()
    {
        menuCanvas.SetActive(true);
        lvlSolvedCanvas.SetActive(false);
        settingMenuCanvas.SetActive(false);
        hintButton.SetActive(false);
        menuButton.SetActive(false);
    }

    public void nextLvlButton()
    {
        MyData.isGamePaused = false;
        int thisScene = SceneManager.GetActiveScene().buildIndex;
        MyData.currentLvL = thisScene + 1;
        SceneManager.LoadScene(thisScene + 1, LoadSceneMode.Single);
    }

    public void retryLvlButton()
    {
        MyData.isGamePaused = false;
        int thisScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(thisScene, LoadSceneMode.Single);
    }
    
    
}
