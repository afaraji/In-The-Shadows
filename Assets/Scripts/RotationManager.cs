using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotationManager : MonoBehaviour
{
	[SerializeField] float _rotSpeed;
	[SerializeField] float _moveFactor;
	[SerializeField] float _maxZ = 0.8f;
	[SerializeField] float _maxY = 0.5f;
	[SerializeField] float _tolerence = 0.5f;
	
	//public Camera cam;
	public Vector3 _initialPos;
	public Quaternion _soloutionRot = Quaternion.identity;
	public Quaternion _currentRot;

	public float solutionPorg;
	
	public float solDelta = 1f;
	
	public RectTransform imageChangeScale;
	public PlaySceneManager manager;


	void Start()
	{
		// rotSpeed = get rot speed from setting
		// isAllowdVerticalRot = get it from setting/difficulty lvl
		if(GetControlFreedom() > 2)
			transform.Translate(0, Random.Range(-0.5f, 0.5f), Random.Range(-0.8f, 0.8f));
		_moveFactor = MyData.moveSpeed;
		MyData.rotationSpeed = PlayerPrefs.GetFloat("RotationSpeed", 1000);
		transform.Rotate(Vector3.up, Random.Range(50f, 360f));
		if(GetControlFreedom() > 1)
			transform.Rotate(Vector3.right, Random.Range(50f, 360f));
		_initialPos = transform.position;
		
		MyData.isGamePaused = false;
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
		//return (MyData.currentLvL);
		return 3;
	}

	public float SolutionProgress(Quaternion R, Vector3 P)
	{
		float x, y, z, dist, tmp;
		
		y = R.eulerAngles.y;
		if (y > 180)
			y = 360 - R.eulerAngles.y;
		
		if (GetControlFreedom() == 1)
		{
			imageChangeScale.transform.localScale = new Vector3(1, Mathf.InverseLerp(0, 180, y), 1);
			return y;
		}

		x = R.eulerAngles.x;
		if (x > 180)
			x = 360 - R.eulerAngles.x;
		z = R.eulerAngles.z;
		if (z > 180)
			z = 360 - R.eulerAngles.z;
		if (GetControlFreedom() == 2)
		{
			tmp = Mathf.InverseLerp(0, 180, x) + Mathf.InverseLerp(0, 180, y) + Mathf.InverseLerp(0, 180, z);
			imageChangeScale.transform.localScale = new Vector3(1, tmp / 3, 1);
			return x + y + z;
		}

		dist = Vector3.Distance(_initialPos, P);
		tmp = Mathf.InverseLerp(0, 180, x) + Mathf.InverseLerp(0, 180, y) + Mathf.InverseLerp(0, 180, z) + Mathf.InverseLerp(0, 0.5f, dist);
		imageChangeScale.transform.localScale = new Vector3(1, tmp / 4, 1);
		return x + y + z + dist;
	}


	private void OnMouseDrag()
	{
		if (MyData.isGamePaused)
			return;
		_rotSpeed = MyData.rotationSpeed;
		float rotX = Input.GetAxis("Mouse X") * _rotSpeed * Time.deltaTime;
		float rotY = Input.GetAxis("Mouse Y") * _rotSpeed * Time.deltaTime;
		Vector3 _pos;
		if (GetControlFreedom() > 2 && Input.GetKey(KeyCode.LeftShift))
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
		else if (GetControlFreedom() > 1 && Input.GetKey(KeyCode.LeftControl))
		{
			transform.rotation = Quaternion.AngleAxis(rotY, Vector3.forward) * transform.rotation;
		}
		else if (GetControlFreedom() > 1 && Input.GetKey(KeyCode.LeftAlt))
		{
			transform.rotation = Quaternion.AngleAxis(rotY, Vector3.right) * transform.rotation;
		}
		else
		{
			transform.rotation = Quaternion.AngleAxis(-rotX, Vector3.up) * transform.rotation;	
		}
		
		/*	----------rotate in consideration of camera position in both direction ------------------
		*	Vector3 right = Vector3.Cross(cam.transform.up, transform.position - cam.transform.position);
		*	Vector3 up = Vector3.Cross(transform.position - cam.transform.position, right);
		*	transform.rotation = Quaternion.AngleAxis(-rotX, up) * transform.rotation;
		*	transform.rotation = Quaternion.AngleAxis(rotY, right) * transform.rotation;
		*/
	}

	private void Update()
	{
		_currentRot = transform.rotation;
		//float progress = SolutionProgress();
		//imageChangeScale.localScale = new Vector3(1, progress, 1);
		solutionPorg = SolutionProgress(_currentRot, transform.position);
		
		if(CmpRot(_currentRot, _soloutionRot, solDelta))
		{
			Debug.Log("Level Solved !");
			manager.OnLevelSolved();
		}
	}

	bool CmpRot(Quaternion rot1, Quaternion rot2, float delta)
	{
		bool x = false, y = false, z = false;
		return false;
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