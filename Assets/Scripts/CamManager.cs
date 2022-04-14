using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamManager : MonoBehaviour
{

    private bool _useMouseToRotateTp;
    public float xRotateSpeed ;
    public float yRotateSpeed ;
    public float zoomSpeed;

    public CinemachineFreeLook thirdPersonCam;
    public CinemachineVirtualCamera firstPersonCam;
    private GameObject player; 
    private GameObject playerFace;
    private GameObject target;
    private GameObject faceCam;

    private bool _isCurrentFp;    

    private void Start()
    {
        player = GameObject.FindWithTag("Player").gameObject;
        target = player.transform.Find("CamTargetPoint").gameObject;
        playerFace = player.transform.Find("FaceTargetPoint").gameObject;

        thirdPersonCam = GameObject.Find("ThirdPersonCam").GetComponent<CinemachineFreeLook>();
        thirdPersonCam.Follow = target.transform;
        thirdPersonCam.LookAt = target.transform;

        firstPersonCam = GameObject.Find("FirstPersonCam").GetComponent<CinemachineVirtualCamera>();
        firstPersonCam.Follow = playerFace.transform;
        firstPersonCam.LookAt = playerFace.transform;

        faceCam = firstPersonCam.transform.Find("FaceCam").gameObject;

        firstPersonCam.gameObject.SetActive(false);
        thirdPersonCam.gameObject.SetActive(true);
        _isCurrentFp = false;
    }

    private void Update()
    {
        // mouse click 
		if(Input.GetMouseButtonDown(1)){
            _useMouseToRotateTp = true;
        }
        if(Input.GetMouseButtonUp(1)){
            _useMouseToRotateTp = false;
        }
        if(Input.GetKeyDown(KeyCode.G)){
            CamChange();
        }
        LateUpdate();
    }

    // update after calculation 
    private void LateUpdate() {
        // rotate option
        if(_useMouseToRotateTp){
            RotateTp();
        } else {
            thirdPersonCam.m_XAxis.m_MaxSpeed = 0;
            thirdPersonCam.m_YAxis.m_MaxSpeed = 0;

            //firstPersonCam.m_XAxis.m_MaxSpeed = 0;
            //firstPersonCam.m_YAxis.m_MaxSpeed = 0;

        }
        if(_isCurrentFp){
            faceCam.transform.position = playerFace.transform.position + player.transform.forward * 0.8f;
            faceCam.transform.LookAt(playerFace.transform.position);
        }
        //mouse scroll for zoom  
        if(Input.mouseScrollDelta.y != 0 && !_isCurrentFp){
            Zoom();
        }
    }

    // rotation velocity 
    private void RotateTp(){
        thirdPersonCam.m_XAxis.m_MaxSpeed= 0;
        thirdPersonCam.m_YAxis.m_MaxSpeed = 0;
        Debug.Log("Rotate");
    }

    // zoom function: boundaries and FieldOfView 
    private void Zoom() {
        if(Input.mouseScrollDelta.y < 0){
            if (thirdPersonCam?.m_Lens.FieldOfView < 80)
            {
                Debug.Log("Zoom out");
                thirdPersonCam.m_Lens.FieldOfView += zoomSpeed;
            }
        }
        if (Input.mouseScrollDelta.y < 0){
            if (thirdPersonCam?.m_Lens.FieldOfView > 5)
            {
                Debug.Log("Zoom in");
                thirdPersonCam.m_Lens.FieldOfView -= zoomSpeed;
            }
        }
    }

    private void CamChange(){
        if(_isCurrentFp){
            firstPersonCam.gameObject.SetActive(false);
            thirdPersonCam.gameObject.SetActive(true);
        }
        else{
            firstPersonCam.gameObject.SetActive(true);
            thirdPersonCam.gameObject.SetActive(false);
        }
        _isCurrentFp = !_isCurrentFp;
    }
}
