using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	public GameObject FreeLookCamObj;
	public CinemachineFreeLook FreeLookCam;
	public float zoomSpeed = 0.5f;
	public float xRotateSpeed = 500.0f;
	public float yRotateSpeed = 10.0f;
	public bool _useMouseToRotateTp;

	private void Start()
	{
		FreeLookCamObj = GameObject.Find("FreeLookCamera");
		FreeLookCam = FreeLookCamObj.GetComponent<CinemachineFreeLook>();
	}
	private void Zoom()
	{
		// Input.mouseScrollDelta.y 값으로 마우스 스크롤 값을 받을 수 있습니다
		// 스크롤을 내리면 이 값이 0보다 작고 스크롤을 올리면 0보다 큽니다
		if (Input.mouseScrollDelta.y < 0)
		{
			// 일정 수준 이상으로 확대,축소되는것을 방지합니다.
			if (FreeLookCam?.m_Lens.FieldOfView < 80)
			{
				Debug.Log("Zoom out");
				// FreeLookCam.m_Lens로 Lens프로퍼티에 접근할 수 있습니다.
				// FieldOfView 값이 크면 멀리서 보이고 작으면 가까이서 보이는 효과를 줍니다.
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
	private void Update()
	{
		// 마우스를 클릭했을때 3인칭 시점변경을 활성화시킵니다.
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
		Zoom();
		if (_useMouseToRotateTp)
		{
			RotateTp();
		}
		else
		{
			FreeLookCam.m_XAxis.m_MaxSpeed = 0;
			FreeLookCam.m_YAxis.m_MaxSpeed = 0;
		}
	}
	private void RotateTp()
	{
		FreeLookCam.m_XAxis.m_MaxSpeed = xRotateSpeed;
		FreeLookCam.m_YAxis.m_MaxSpeed = yRotateSpeed;
	}
}
