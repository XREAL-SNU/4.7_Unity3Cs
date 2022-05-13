using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public GameObject freeLookCamObj;
    public CinemachineFreeLook freeLookCam;
    public float zoomSpeed = 0.5f;
    public float yRotateSpeed=10.0f;
    public float xRotateSpeed=500.0f;
    private bool _useMouseToRotateTp=false;
    private void Start()
    {
        freeLookCamObj = GameObject.Find("CM FreeLook1");
        freeLookCam = freeLookCamObj.GetComponent<CinemachineFreeLook>();
    }
    private void Update()
    {
        //마우스 클릭했을 때에만 시점 변경
        if(Input.GetMouseButtonDown(1))
        {
            _useMouseToRotateTp=true;
        }
        if(Input.GetMouseButtonUp(1))
        {
            _useMouseToRotateTp=false;
        }
    }
    private void LateUpdate()
    {
        Zoom();
        if(_useMouseToRotateTp)
        {
            RotateTp();
        }
        else
        {
            //FreeLookCam 의 일반 마우스 움직임 비활성화
            freeLookCam.m_XAxis.m_MaxSpeed=0;
            freeLookCam.m_YAxis.m_MaxSpeed=0;
        }
    }

    private void Zoom()
    {
        if (Input.mouseScrollDelta.y < 0)//마우스 입력값 받기
        {
            //일정 수준 이상으로 확대 축소되는 것 방지
            if (freeLookCam?.m_Lens.FieldOfView < 80)//일정 수준으로 확대 축소되는 것 방지
            {
                //FieldOfView로 크면 멀리서 보이고 작으면 가까이보임
                Debug.Log("Zoom out");
                freeLookCam.m_Lens.FieldOfView += zoomSpeed;
            }
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            if (freeLookCam?.m_Lens.FieldOfView > 5)
            {
                Debug.Log("Zoom in");
                freeLookCam.m_Lens.FieldOfView -= zoomSpeed;
            }
        }
    }
    private void RotateTp()
    {
        freeLookCam.m_XAxis.m_MaxSpeed = xRotateSpeed;
        freeLookCam.m_YAxis.m_MaxSpeed = yRotateSpeed;
    }
}
