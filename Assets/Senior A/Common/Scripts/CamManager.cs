using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamManager : MonoBehaviour
{
	private GameObject player;
	private GameObject FreeLookCamobj;
    private CinemachineFreeLook FreeLookCam;
	private bool _useMouseToRotateTp;
	private bool _isCurrentFp;

	private void Start()
	{
		
		player = GameObject.FindWithTag("Player").gameObject;
		// player scene에 소환. 

		FreeLookCam = GameObject.Find("FreeLookCamera").GetComponent<CinemachineFreeLook>();
		FreeLookCam.Follow = player.transform;
		FreeLookCam.LookAt = player.transform;
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
	}

	private void LateUpdate()
	{
		if (_useMouseToRotateTp)
		{
			RotateTp();
		}
		else
		{
			FreeLookCam.m_XAxis.m_MaxSpeed = 0;
			FreeLookCam.m_YAxis.m_MaxSpeed = 0;
		}

		if (Input.mouseScrollDelta.y != 0 && !_isCurrentFp)
		{
			Zoom();
		}
	}

	public float xRotateSpeed;
	public float yRotateSpeed;

	private void RotateTp()
	{
		FreeLookCam.m_XAxis.m_MaxSpeed = xRotateSpeed;
		FreeLookCam.m_YAxis.m_MaxSpeed = yRotateSpeed;
	}

	public float zoomSpeed;
	private void Zoom()
	{
		if (Input.mouseScrollDelta.y < 0)
		{
			if (FreeLookCam?.m_Lens.FieldOfView < 80)
			{
				Debug.Log("Zoom out");
				FreeLookCam.m_Lens.FieldOfView += zoomSpeed;
			}
		}
		if (Input.mouseScrollDelta.y > 0)
		{
			if (FreeLookCam?.m_Lens.FieldOfView > 5)
			{
				Debug.Log("Zoom in");
				FreeLookCam.m_Lens.FieldOfView -= zoomSpeed;
			}
		}
	}
}