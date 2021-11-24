using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaySceneManager : MonoBehaviour
{
    public int numberOfObjects = 1;
    public GameObject hintButton;
    public GameObject menuButton;
    public GameObject menuCanvas;
    public GameObject lvlSolvedCanvas;
    public GameObject settingMenuCanvas;
    //public Image starsOnSolved;

    void Start()
    {
        menuCanvas.SetActive(false);
        lvlSolvedCanvas.SetActive(false);
        settingMenuCanvas.SetActive(false);
    }

    public void hntButtonPressed()
    {
        Debug.Log("show hint for : " + SceneManager.GetActiveScene().name);
    }

    public void pauseButton()
    {
        menuCanvas.SetActive(true);
        lvlSolvedCanvas.SetActive(false);
        settingMenuCanvas.SetActive(false);
        hintButton.SetActive(false);
        menuButton.SetActive(false);
        // pause game = 1
    }
    
    public void mainMenuButton()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void settingButton()
    {
        menuCanvas.SetActive(false);
        lvlSolvedCanvas.SetActive(false);
        settingMenuCanvas.SetActive(true);
        hintButton.SetActive(false);
        menuButton.SetActive(false);
        // pause game = 1
    }

    public void continueButton()
    {
        menuCanvas.SetActive(false);
        lvlSolvedCanvas.SetActive(false);
        settingMenuCanvas.SetActive(false);
        hintButton.SetActive(true);
        menuButton.SetActive(true);
        // pause game = 0
    }

    public void OnLevelSolved()
    {
        menuCanvas.SetActive(false);
        lvlSolvedCanvas.SetActive(true);
        settingMenuCanvas.SetActive(false);
        hintButton.SetActive(false);
        menuButton.SetActive(false);
        
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
        int thisScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(thisScene + 1, LoadSceneMode.Single);
    }

    public void retryLvlButton()
    {
        int thisScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(thisScene, LoadSceneMode.Single);
    }
    
    
}
