using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamManager : MonoBehaviour
{
	private GameObject player;
	private CinemachineFreeLook thirdPersonCam;
	bool _useMouseToRotateTp;

	
	CinemachineVirtualCamera firstPersonCam;
	GameObject playerFace;

	private GameObject faceCam;

	private void Start()
	{
		//player = GameObject.Find("Character/Head").gameObject;
		player = GameObject.FindWithTag("Player").gameObject;
		playerFace = GameObject.Find("Character/Head").gameObject;

		firstPersonCam = GameObject.Find("FirstPersonCam").GetComponent<CinemachineVirtualCamera>();
		firstPersonCam.Follow = playerFace.transform;
		firstPersonCam.LookAt = playerFace.transform;
		faceCam = firstPersonCam.transform.Find("FaceCam").gameObject;

		thirdPersonCam = GameObject.Find("ThirdPersonCam").GetComponent<CinemachineFreeLook>();
		thirdPersonCam.Follow = player.transform;
		thirdPersonCam.LookAt = player.transform;

		firstPersonCam.gameObject.SetActive(false);
		thirdPersonCam.gameObject.SetActive(true);
		_isCurrentFp = false;
	}
	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			_useMouseToRotateTp = true;
		}
		if (Input.GetMouseButtonUp(1))
		{
			_useMouseToRotateTp = false;
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			CamChange();
		}
	}
	private void CamChange()
	{
		if (_isCurrentFp)
		{
			firstPersonCam.gameObject.SetActive(false);
			thirdPersonCam.gameObject.SetActive(true);
		}
		else
		{
			firstPersonCam.gameObject.SetActive(true);
			thirdPersonCam.gameObject.SetActive(false);
		}
		_isCurrentFp = !_isCurrentFp;
	}
	bool _isCurrentFp;
	private void LateUpdate()
	{
		if (_useMouseToRotateTp)
		{
			RotateTp();
		}
		else
		{
			thirdPersonCam.m_XAxis.m_MaxSpeed = 0;
			thirdPersonCam.m_YAxis.m_MaxSpeed = 0;
		}
		//스크롤 다운일 경우 0보다 작고, 스크롤 업일 경우 0보다 큼
		if (Input.mouseScrollDelta.y != 0 && !_isCurrentFp)
		{
			Zoom();
		}

		if (_isCurrentFp)
		{
			faceCam.transform.position = playerFace.transform.position + player.transform.forward * 0.8f;
			faceCam.transform.LookAt(playerFace.transform.position);
		}
	}
	public float xRotateSpeed;
	public float yRotateSpeed;

	private void RotateTp()
	{
		thirdPersonCam.m_XAxis.m_MaxSpeed = xRotateSpeed;
		thirdPersonCam.m_YAxis.m_MaxSpeed = yRotateSpeed;
	}

	public float zoomSpeed;
	private void Zoom()
	{
		if (Input.mouseScrollDelta.y < 0)
		{
			if (thirdPersonCam?.m_Lens.FieldOfView < 80)
			{
				thirdPersonCam.m_Lens.FieldOfView += zoomSpeed;
			}
		}
		if (Input.mouseScrollDelta.y > 0)
		{
			if (thirdPersonCam?.m_Lens.FieldOfView > 5)
			{
				thirdPersonCam.m_Lens.FieldOfView -= zoomSpeed;
			}
		}
	}
}