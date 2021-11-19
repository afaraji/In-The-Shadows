using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
	public PlayerData playerData;
	void Start()
	{
		playerData = new PlayerData();
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}

public class PlayerData
{
	public bool isModeNormal = true;
	public int currentLvL = 1;
	public float generalSound = 0.5f;
	public float musicVolume = 0.2f;
	public float effectsVolume = 0.2f;
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
