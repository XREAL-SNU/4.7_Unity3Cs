using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float runSpeed = 3.0f;
    public bool _isPunch;

    private Rigidbody _charRigidbody;
    private Animator _animator;

    void Start()
    {
        _charRigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        bool isRun = Input.GetKey(KeyCode.LeftShift);
        Punch();

        // �̵��ϰ��� �ϴ� ���� ���
        Vector3 inputDir = new Vector3(hAxis, 0, vAxis).normalized;

        // �̵��ϴ� �������� ȸ��
        transform.LookAt(transform.position + inputDir);

        if (!_isPunch) { 
            if (!isRun)
            {
                _charRigidbody.MovePosition(_charRigidbody.position + inputDir * moveSpeed * Time.deltaTime);
            }
            else
            {
                _charRigidbody.MovePosition(_charRigidbody.position + inputDir * runSpeed * Time.deltaTime);
            }

            _animator.SetBool("isWalking", inputDir != Vector3.zero);
            _animator.SetBool("isRunning", isRun);
        }
    }

    void Punch()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !_isPunch)
        {
            _isPunch = true;
            _animator.SetTrigger("Punch");

            Invoke("ResetTrigger", 1f);

            Debug.Log("Punch");
        }
    }

    void ResetTrigger()
    {
        _isPunch = false;
    }
}