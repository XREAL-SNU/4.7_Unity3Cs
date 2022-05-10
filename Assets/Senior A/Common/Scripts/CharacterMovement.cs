using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class CharacterMovement : MonoBehaviourPunCallbacks
{
    float moveSpeed = 1f;
    float runSpeed = 3.0f;
    public float jumpPower = 5.0f;

    private bool _isJump;
    private bool _isPunch;

    Rigidbody _charRigidbody;

    private Animator _animator;

    public static bool isGoing;

    protected virtual void Awake()
    {
        _charRigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

    }

    public virtual void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        bool isRun = Input.GetKey(KeyCode.LeftShift);

        // 이동하고자 하는 방향 계산
        Vector3 inputDir = new Vector3(hAxis, 0, vAxis).normalized;

        // 이동하는 방향으로 회전
        transform.LookAt(transform.position + inputDir);
        if (!_isPunch)
        {
            if (!isRun)
            {
                _charRigidbody.MovePosition(_charRigidbody.transform.position + inputDir * moveSpeed * Time.deltaTime);
            }
            else
            {
                _charRigidbody.MovePosition(_charRigidbody.transform.position + inputDir * runSpeed * Time.deltaTime);
            }
        }
        

        _animator.SetBool("isWalking", inputDir != Vector3.zero);
        _animator.SetBool("isRunning", isRun);

        Jump();
        Punch();
        if (isGoing) Going();
     
    }

    protected void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isJump && !_isPunch)
        {
            _isJump = true;
            _animator.SetTrigger("Jump");
            _charRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            Invoke("ResetTrigger", 1f);
        }
    }
    protected void Punch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !_isJump && !_isPunch)
        {
            _isPunch = true;
            _animator.SetTrigger("Punch");

            Invoke("ResetTrigger", 0.5f);
        }
    }
    protected void ResetTrigger()
    {
        _isJump = false;
        _isPunch = false;
    }
    Transform targetTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potal"))
        {
            isGoing = true;
            targetTransform = other.transform;
            other.GetComponent<BoxCollider>().enabled = false;
            transform.DOLocalRotate(new Vector3(0, 1080, 0), 2f, RotateMode.FastBeyond360).SetEase(Ease.OutQuart);
            other.GetComponent<PotalDOTween>().EnterPlayer = true;
            
        }
    }

    void Going()
    {
        transform.position = targetTransform.transform.position;
    }
}
