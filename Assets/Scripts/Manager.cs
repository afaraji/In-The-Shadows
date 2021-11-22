using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
	public PlayerData playerData;
	public int totalLevels = 6;
	void Start()
	{
		playerData = new PlayerData();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void LoadLvlMenu(int unlockedLvl, int maxLvl)
	{
		SceneManager.LoadScene("LevelsMenu", LoadSceneMode.Additive);
		// was here trying to lucng menu for test/normal mode
		
	}

	public void PlayInTestMode()
	{
		Debug.Log("test Mode");
	}

	public void PlayInNormalMode()
	{
		Debug.Log("normal Mode");
	}

	public void OpenSettings()
	{
		Debug.Log("settings");
	}

	public void ExitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}
}

public class PlayerData
{
	public bool isModeNormal = true;
	public int currentLvL = 1;
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
}
