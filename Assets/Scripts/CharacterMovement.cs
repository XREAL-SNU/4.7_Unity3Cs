using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController _controller;
    private float _speed = 5f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!_controller.enabled)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            _controller.Move(direction * _speed * Time.deltaTime);
        }
    }
}
