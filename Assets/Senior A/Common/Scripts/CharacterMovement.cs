using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
private Rigidbody _charRigidbody;
private Animator _animator;

void Start()
{
        _charRigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
}    void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

				// 이동하고자 하는 방향 계산
        Vector3 inputDir = new Vector3(hAxis, 0, vAxis).normalized;

        _charRigidbody.MovePosition(_charRigidbody.position + inputDir * moveSpeed * Time.deltaTime);
					
				// 이동하는 방향으로 회전
        transform.LookAt(transform.position + inputDir);
        _animator.SetBool("isWalking", inputDir != Vector3.zero);
    }
}