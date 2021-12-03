using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject lvlMenu;
	public GameObject settingMenu;
	public LevelSelectMenu levelSelectMenu;
	public int totalLevels = 6;
	
	void Start()
	{
		mainMenu.SetActive(true);
		lvlMenu.SetActive(false);
		settingMenu.SetActive(true);
		PlayerPrefs.SetInt("TotalLvls", totalLevels);
		
		//DontDestroyOnLoad(music);
		//DontDestroyOnLoad(buttonSound);
		settingMenu.SetActive(false);
	}

	


	public void PlayInTestMode()
	{
		AudioManager.audioManager.PlayClick();
		MyData.isModeNormal = false;
		mainMenu.SetActive(false);
		lvlMenu.SetActive(true);
		settingMenu.SetActive(false);
		levelSelectMenu.RefreshButtons(totalLevels, totalLevels);
		
	}

	public void PlayInNormalMode()
	{
		AudioManager.audioManager.PlayClick();
		MyData.isModeNormal = true;
		mainMenu.SetActive(false);
		lvlMenu.SetActive(true);
		settingMenu.SetActive(false);
		levelSelectMenu.RefreshButtons(PlayerPrefs.GetInt("UnlockedLvl", 1), totalLevels);
	}

	public void OpenSettings()
	{
		AudioManager.audioManager.PlayClick();
		mainMenu.SetActive(false);
		lvlMenu.SetActive(false);
		settingMenu.SetActive(true);
	}

	public void ExitGame()
	{
		AudioManager.audioManager.PlayClick();
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}

	public void OnBackToMainMenu()
	{
		AudioManager.audioManager.PlayClick();
		mainMenu.SetActive(true);
		lvlMenu.SetActive(false);
		settingMenu.SetActive(false);
		SceneManager.LoadScene(0);
	}

	public void ClearData()
	{
		AudioManager.audioManager.PlayClick();
		PlayerPrefs.DeleteAll();
	}
}

/*public class PlayerData
{
	public bool isModeNormal = true;
	public int currentLvL = 2;
	//float rotationSpeed = 0.5f;
	//float moveSpeed = 0.5f;
	public float rotationSpeed
	{
		get{return rotationSpeed * 1600 + 200;}
		set{rotationSpeed = (value - 200)/1600;}
	}
	public float moveSpeed
	{
		get{return moveSpeed * 80 + 80;}
		set{moveSpeed = value / 80 + 1;}
	}
}*/

/*
 * ************  PlayerPrefs content *************
 * GeneralVolume
 * SFX
 * Music
 * RotationSpeed
 * UnlockedLvl
 * TotalLvls
 */