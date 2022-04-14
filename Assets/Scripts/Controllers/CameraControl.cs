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
		// Input.mouseScrollDelta.y ������ ���콺 ��ũ�� ���� ���� �� �ֽ��ϴ�
		// ��ũ���� ������ �� ���� 0���� �۰� ��ũ���� �ø��� 0���� Ů�ϴ�
		if (Input.mouseScrollDelta.y < 0)
		{
			// ���� ���� �̻����� Ȯ��,��ҵǴ°��� �����մϴ�.
			if (FreeLookCam?.m_Lens.FieldOfView < 80)
			{
				Debug.Log("Zoom out");
				// FreeLookCam.m_Lens�� Lens������Ƽ�� ������ �� �ֽ��ϴ�.
				// FieldOfView ���� ũ�� �ָ��� ���̰� ������ �����̼� ���̴� ȿ���� �ݴϴ�.
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
		// ���콺�� Ŭ�������� 3��Ī ���������� Ȱ��ȭ��ŵ�ϴ�.
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
