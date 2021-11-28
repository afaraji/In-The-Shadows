using System.Collections;
using System.Collections.Generic;
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
    public ModelData[] model;
    public RotationManager[] models;
    public RectTransform imageChangeScale;
    public float difficulty;
    
    
    //public Image starsOnSolved;

    void Start()
    {
        menuCanvas.SetActive(false);
        lvlSolvedCanvas.SetActive(false);
        settingMenuCanvas.SetActive(false);
    }

    private void LateUpdate()
    {
        float totalProgress = 0;
        foreach (var m in models)
        {
            totalProgress += m.solutionPorg;
        }

        totalProgress /= models.Length;
        imageChangeScale.transform.localScale = new Vector3(1, totalProgress, 1);
        if (totalProgress < difficulty / 100)
            OnLevelSolved();
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

    public void OnLevelSolved()
    {
        menuCanvas.SetActive(false);
        lvlSolvedCanvas.SetActive(true);
        settingMenuCanvas.SetActive(false);
        hintButton.SetActive(false);
        menuButton.SetActive(false);
        MyData.isGamePaused = true;
        // if last level disable next lvl button
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
        SceneManager.LoadScene(thisScene + 1, LoadSceneMode.Single);
    }

    public void retryLvlButton()
    {
        MyData.isGamePaused = false;
        int thisScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(thisScene, LoadSceneMode.Single);
    }
    
    
}
