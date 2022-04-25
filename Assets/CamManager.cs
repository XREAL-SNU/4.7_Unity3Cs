using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamManager : MonoBehaviour
{
    private GameObject player;
    private CinemachineFreeLook thirdPersonCam;
    public GameObject lookAt;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").gameObject;
        //lookAt = GameObject.Find("LookAt").gameObject;
        thirdPersonCam = GameObject.Find("ThirdPersonCam").GetComponent<CinemachineFreeLook>();
        thirdPersonCam.Follow = player.transform;
        thirdPersonCam.LookAt = player.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
