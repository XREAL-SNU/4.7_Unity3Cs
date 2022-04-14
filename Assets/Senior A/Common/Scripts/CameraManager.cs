using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    private GameObject player;
    private CinemachineFreeLook thirdPersonCamera;
    private GameObject playerLookAt;
    private GameObject playerFollow;

    private bool isMouseRotate = false;
    private float xRotateSpeed = 300f;
    private float yRotateSpeed = 10f;

    private float zoomspeed = 2f;

    public void Start() {
        player = GameObject.FindWithTag("Player").gameObject;
        thirdPersonCamera = GameObject.Find("ThirdPersonCamera").GetComponent<CinemachineFreeLook>();
        playerLookAt = player.transform.Find("LookAtPoint").gameObject;
        thirdPersonCamera.Follow = player.transform;
        thirdPersonCamera.LookAt = playerLookAt.transform;
    }

    public void Update() {
        if(Input.GetMouseButtonDown(1)) {
            isMouseRotate = true;
        }
        if(Input.GetMouseButtonUp(1)) {
            isMouseRotate = false;
        }
    }

    public void LateUpdate() {
        RotateLookAt(isMouseRotate);
        Zoom(Input.mouseScrollDelta.y);
    }

    private void RotateLookAt(bool isMouseRotate) {
        float _xRotateSpeed = 0f;
        float _yRotateSpeed = 0f;
        if(isMouseRotate) {
            _xRotateSpeed = xRotateSpeed;
            _yRotateSpeed = yRotateSpeed;
        }
        thirdPersonCamera.m_XAxis.m_MaxSpeed = _xRotateSpeed;
        thirdPersonCamera.m_YAxis.m_MaxSpeed = _yRotateSpeed;
    }

    private void Zoom(float zoomDirection) {
        if(zoomDirection > 0) {
            thirdPersonCamera.m_Lens.FieldOfView = Mathf.Min(thirdPersonCamera.m_Lens.FieldOfView + zoomspeed, 50);
        } else if(zoomDirection < 0) {
            thirdPersonCamera.m_Lens.FieldOfView = Mathf.Max(thirdPersonCamera.m_Lens.FieldOfView - zoomspeed, 5);
        }
    }
}
