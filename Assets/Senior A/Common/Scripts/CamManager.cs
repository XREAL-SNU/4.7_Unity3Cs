using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamManager : MonoBehaviour
{
	private GameObject player;
	private CinemachineFreeLook thirdPersonCam;
	private bool _useMouseToRotateTp;
	public float xRotateSpeed;
	public float yRotateSpeed;

	public CinemachineVirtualCamera firstPersonCam;
	public GameObject playerFace;

	private GameObject faceCam;
	private bool _isCurrentFp;

	private void Start()
	{
		// 카메라가 쫓아다닐 플레이어를 설정합니다.
		player = GameObject.FindWithTag("Player");
		// 


		playerFace = player.transform.Find("FollowTarget").gameObject;
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
	private void LateUpdate()
	{
		if (_useMouseToRotateTp)
		{
			RotateTp();
			Debug.Log("Rotate");
		}
		else
		{
			thirdPersonCam.m_XAxis.m_MaxSpeed = 0;
			thirdPersonCam.m_YAxis.m_MaxSpeed = 0;
		}

		if (Input.mouseScrollDelta.y != 0)
		{
			Zoom();
		}

		if (_isCurrentFp)
		{
			faceCam.transform.position = playerFace.transform.position + player.transform.forward * 2f;
			faceCam.transform.LookAt(playerFace.transform.position);
		}
	}


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
				Debug.Log("Zoom out");
				thirdPersonCam.m_Lens.FieldOfView += zoomSpeed;
			}
		}
		if (Input.mouseScrollDelta.y > 0)
		{
			if (thirdPersonCam?.m_Lens.FieldOfView > 5)
			{
				Debug.Log("Zoom in");
				thirdPersonCam.m_Lens.FieldOfView -= zoomSpeed;
			}
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
}