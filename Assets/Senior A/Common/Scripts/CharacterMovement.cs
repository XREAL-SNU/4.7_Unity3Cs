using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    private Rigidbody _charRigidbody;
    private Animator _animator;
    void Start(){
        _charRigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        if(_charRigidbody == null || _animator == null)
            Debug.Log("null");

    }
    void Update(){
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(hAxis, 0, vAxis).normalized;
        //transform.position += Time.deltaTime * moveSpeed * inputDir;
        _charRigidbody.MovePosition(_charRigidbody.position + Time.deltaTime * moveSpeed * inputDir);
        transform.LookAt(transform.position + inputDir);

        _animator.SetBool("isWalking", inputDir != Vector3.zero);
    }
}
