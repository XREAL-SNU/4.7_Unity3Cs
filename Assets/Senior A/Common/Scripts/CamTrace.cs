using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrace : MonoBehaviour
{
    PlayerControllerMulti player;
    public Vector3 offset;
    float followSpeed = 1f;
    void Start()
    {
        foreach (var item in FindObjectsOfType<PlayerControllerMulti>())
        {
            if (item._view.IsMine) player = item ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camera_pos = player.transform.position + offset;
        Vector3 lerp_pos = Vector3.Lerp(transform.position, camera_pos, followSpeed);
        transform.position = lerp_pos;
        transform.LookAt(player.transform);
    }
}
