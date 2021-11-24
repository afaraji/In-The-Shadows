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
	public int _unlockedLevel = 2;
	public bool restAll = false;
	
	
	void Start()
	{
		mainMenu.SetActive(true);
		lvlMenu.SetActive(false);
		settingMenu.SetActive(false);
		PlayerPrefs.SetInt("TotalLvls", totalLevels);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void PlayInTestMode()
	{
		PlayerPrefs.SetInt("isNormalMode", 0);
		mainMenu.SetActive(false);
		lvlMenu.SetActive(true);
		settingMenu.SetActive(false);
		levelSelectMenu.RefreshButtons(totalLevels, totalLevels);
	}

	public void PlayInNormalMode()
	{
		PlayerPrefs.SetInt("isNormalMode", 1);
		mainMenu.SetActive(false);
		lvlMenu.SetActive(true);
		settingMenu.SetActive(false);
		//levelSelectMenu.RefreshButtons(PlayerPrefs.GetInt("UnlockedLvl", 1), totalLevels);
		levelSelectMenu.RefreshButtons(_unlockedLevel, totalLevels);
	}

	public void OpenSettings()
	{
		mainMenu.SetActive(false);
		lvlMenu.SetActive(false);
		settingMenu.SetActive(true);
	}

	public void ExitGame()
	{
		if (restAll)
		{
			PlayerPrefs.DeleteAll();
			restAll = false;
			return;
		}
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}

	public void OnBackToMainMenu()
	{
		Debug.Log("back to main menu");
		mainMenu.SetActive(true);
		lvlMenu.SetActive(false);
		settingMenu.SetActive(false);
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
 * isNormalMode
 */