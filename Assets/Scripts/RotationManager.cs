using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
	[SerializeField] float _rotSpeed;
	[SerializeField] float _moveFactor;
	[SerializeField] float _maxZ = 0.8f;
	[SerializeField] float _maxY = 0.5f;
	[SerializeField] float _tolerence = 0.5f;
	Vector3 _pos;
	public Camera cam;
	private Vector3 _initialPos;
	public Quaternion _soloutionRot;
	public Quaternion _currentRot;
	
	public bool isSolutionRot = false;
	public bool setSolutionRot = false;
	public bool allowSolution = false;
	public float solDelta = 5f;
	
	public RectTransform imageChangeScale;
	public PlaySceneManager manager;


	void Start()
	{
		// rotSpeed = get rot speed from setting
		// isAllowdVerticalRot = get it from setting/difficulty lvl
		_moveFactor = 100f;
		_rotSpeed = PlayerPrefs.GetFloat("RotationSpeed", 0);
		_initialPos = transform.position;
	}

	private int GetControlFreedom()
	{
		/*
		 * if test mode : all mvt and rot allowed
		 * else
		 * 		if lvl == 1 :  horizontal rot allowed
		 * 		if lvl == 2 :  horizontal + vertical rot allowed
		 * 		else all allowed
		 */
		int mode = PlayerPrefs.GetInt("isNormalMode", -1);
		if (mode == -1)
			throw new System.Exception("Mode not set properly");
		if (mode == 0)
			return 3;
		return (MyData.currentLvL);
	}

	public float SolutionProgress()
	{
		float x = Mathf.Abs(transform.rotation.x - _soloutionRot.x) * _tolerence;
		float y = Mathf.Abs(transform.rotation.y - _soloutionRot.y) * _tolerence;
		float z = Mathf.Abs(transform.rotation.z - _soloutionRot.z) * _tolerence;
		float w = Mathf.Abs(transform.rotation.w - _soloutionRot.w) * _tolerence;

		x = Mathf.InverseLerp(0, 25, x); // 0 -> identical | 1 -> far
		y = Mathf.InverseLerp(0, 25, y);
		z = Mathf.InverseLerp(0, 25, z);
		w = Mathf.InverseLerp(0, 25, w);
		// x+y+z+w == 4 -> far | 0 -> close
		return (x + y + z + w)/4;
	}


	private void OnMouseDrag()
	{

		float rotX = Input.GetAxis("Mouse X") * _rotSpeed * Time.deltaTime;
		float rotY = Input.GetAxis("Mouse Y") * _rotSpeed * Time.deltaTime;

		if (GetControlFreedom() > 2 && Input.GetKey(KeyCode.LeftControl))
		{

			Vector3 right = Vector3.Cross(Vector3.up, Vector3.left);
			Vector3 up = Vector3.Cross(Vector3.left, right);
			_pos = transform.position + right * (rotX/_moveFactor) + up * (rotY/_moveFactor);

			if (_pos.z > _maxZ + _initialPos.z)
				_pos.z = _maxZ + _initialPos.z;
			if (_pos.z < -_maxZ + _initialPos.z)
				_pos.z = -_maxZ + _initialPos.z;
			if (_pos.y > _maxY + _initialPos.y)
				_pos.y = _maxY + _initialPos.y;
			if (_pos.y < -_maxY + _initialPos.y)
				_pos.y = -_maxY + _initialPos.y;
			transform.position = _pos;
		}
		else
		{
			if (GetControlFreedom() == 1)
				rotY = 0;

			Vector3 right = Vector3.Cross(cam.transform.up, transform.position - cam.transform.position);
			Vector3 up = Vector3.Cross(transform.position - cam.transform.position, right);
			transform.rotation = Quaternion.AngleAxis(-rotX, up) * transform.rotation;
			transform.rotation = Quaternion.AngleAxis(rotY, right) * transform.rotation;
		}
	}

	private void Update()
	{
		if (!isSolutionRot)
		{
			if (setSolutionRot)
			{
				isSolutionRot = true;
				setSolutionRot = false;
				_soloutionRot = transform.rotation;
			}
			return;
		}

		_currentRot = transform.rotation;
		//float progress = SolutionProgress();
		//imageChangeScale.localScale = new Vector3(1, progress, 1);
		if(CmpRot(_currentRot, _soloutionRot, solDelta) && allowSolution)
		{
			Debug.Log("Level Solved !");
			manager.OnLevelSolved();
		}
	}

	bool CmpRot(Quaternion rot1, Quaternion rot2, float delta)
	{
		bool x = false, y = false, z = false;
		float tmp = Mathf.Abs(rot1.eulerAngles.x - rot2.eulerAngles.x);
		tmp = tmp < 180 ? tmp : 360 - tmp;
		if (tmp <= delta)
			x = true;
		tmp = Mathf.Abs(rot1.eulerAngles.y - rot2.eulerAngles.y);
		tmp = tmp < 180 ? tmp : 360 - tmp;
		if (tmp <= delta)
			y = true;
		tmp = Mathf.Abs(rot1.eulerAngles.z - rot2.eulerAngles.z);
		tmp = tmp < 180 ? tmp : 360 - tmp;
		if (tmp <= delta)
			z = true;
		
		if (x && y && z)
			return true;
		return false;
	}
}


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