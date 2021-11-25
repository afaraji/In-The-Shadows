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
    
    
    //public Image starsOnSolved;

    void Start()
    {
        menuCanvas.SetActive(false);
        lvlSolvedCanvas.SetActive(false);
        settingMenuCanvas.SetActive(false);
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
        Debug.Log("show hint for : " + SceneManager.GetActiveScene().name);
        string hintText = "test text";
        StartCoroutine(ShowHint(hintText));
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
