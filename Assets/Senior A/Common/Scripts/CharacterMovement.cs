using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    private Rigidbody _charRigidbody;
    private Animator _animator;
    public float runSpeed = 3.0f;
    public float jumpPower = 5.0f;

    private bool _isJump;
    private bool _isPunch;

    // Start is called before the first frame update
    void Start()
    {
        _charRigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");
        bool isRun = Input.GetKey(KeyCode.LeftShift);

        Jump();
        Punch();

        Vector3 inputDir = new Vector3(hAxis, 0, vAxis).normalized;
        transform.LookAt(transform.position + inputDir);
        if (!_isPunch)
        {
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

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isJump && !_isPunch)
        {
            _isJump = true;
            _animator.SetTrigger("Jump");
            _charRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            Invoke("ResetTrigger", 1f);
        }
    }

    void Punch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !_isJump && !_isPunch)
        {
            _isPunch = true;
            _animator.SetTrigger("Punch");

            Invoke("ResetTrigger", 0.5f);
        }
    }

    void ResetTrigger()
    {
        _isJump = false;
        _isPunch = false;
    }
}
