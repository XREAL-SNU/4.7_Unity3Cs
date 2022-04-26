using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    private Rigidbody _charRigidBody;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _charRigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(hAxis, 0, vAxis).normalized;
        _charRigidBody.MovePosition(_charRigidBody.position + inputDir * moveSpeed * Time.deltaTime);
        transform.LookAt(transform.position + inputDir);

        _animator.SetBool("isWalking", inputDir != Vector3.zero);

    }
}
