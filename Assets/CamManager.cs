using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamManager : MonoBehaviour
{
    private GameObject player;
    private CinemachineFreeLook thirdPersonCam;

    // Start is called before the first frame update
    void Start()
    {
        // 카메라가 쫓아다닐 플레이어를 설정합니다.
        player = GameObject.FindWithTag("Player");
        // 
        thirdPersonCam = GameObject.Find("ThirdPersonCam").GetComponent<CinemachineFreeLook>();
        thirdPersonCam.Follow = player.transform;
        thirdPersonCam.LookAt = player.transform;
    }

    private bool _useMouseToRotateTp;

    // Update is called once per frame
    void Update()
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

    private bool _isCurrentFp;

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

        if (Input.mouseScrollDelta.y != 0 && !_isCurrentFp)
        {
            Zoom();
        }
    }

    public float xRotateSpeed = 1f;
    public float yRotateSpeed = 1f;

    private void RotateTp()
    {
        thirdPersonCam.m_XAxis.m_MaxSpeed = xRotateSpeed;
        thirdPersonCam.m_YAxis.m_MaxSpeed = yRotateSpeed;
    }

    private float zoomSpeed = 1.5f;

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
}
