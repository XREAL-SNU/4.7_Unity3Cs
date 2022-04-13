using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour
{
    float moveSpeed = 1f;
    float runSpeed = 3.0f;
    public float jumpPower = 5.0f;

    private bool _isJump;
    private bool _isPunch;

    Rigidbody _charRigidbody;

    private Animator _animator;

    public static bool isGoing;

    private Vector3 _pos;
    float _targetX = 3;
    float _targetY = 3;
    float _tweeningDuration = 2;

    Sequence _mySequence;

    void Start()
    {
        _charRigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        //_mySequence = DOTween.Sequence().Append(transform.DOMoveX(_targetX, _tweeningDuration).SetEase(Ease.Linear)).Append(transform.DOMoveY(_targetY, _tweeningDuration).SetEase(Ease.Linear));

    }

    void Update()
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
                _charRigidbody.MovePosition(_charRigidbody.position + inputDir * moveSpeed * Time.deltaTime);
            }
            else
            {
                _charRigidbody.MovePosition(_charRigidbody.position + inputDir * runSpeed * Time.deltaTime);
            }
        }
        

        _animator.SetBool("isWalking", inputDir != Vector3.zero);
        _animator.SetBool("isRunning", isRun);

        Jump();
        Punch();
        if (isGoing) Going();
     
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
       // transform.SetParent(targetTransform);
        transform.position = targetTransform.transform.position;
    }
}
