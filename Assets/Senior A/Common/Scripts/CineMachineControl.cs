using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineMachineControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    float cameraDisrance;
    CinemachineComponentBase componentBase;
    public GameObject player;
    [SerializeField] float sensitivity = 10f;
    bool isMouseDragging = false;
    private Vector3 startPosition;
    private Vector3 rotateVector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(componentBase == null)
        {
            componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        }

        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            cameraDisrance = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            if(componentBase is CinemachineFramingTransposer)
            {
                (componentBase as CinemachineFramingTransposer).m_CameraDistance -= cameraDisrance;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("mouse button down");
            startPosition = Input.mousePosition;
            isMouseDragging = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isMouseDragging = false;
        }

        if(isMouseDragging)
        {

            Debug.Log(startPosition);
            Debug.Log(Input.mousePosition);

            rotateVector = Input.mousePosition - startPosition;
            rotateVector.z = 0;
            rotateVector.x = 0;
            
            player.transform.rotation = Quaternion.Euler(-1 * rotateVector);
        }
    }

    
   
}
