using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerThirdPerson : MonoBehaviour
{
    public float MoveSpeed = 10.0f;
    public float RunSpeed = 20.0f;
    public float JumpHeight = 2.0f;
    public LayerMask GroundLayers;

    protected float _speedChangeRate = 10.0f;
    protected float _rotationSmoothTime = 0.12f;
    protected float _groundCheckRadius = 0.28f;

    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private Rigidbody _charRigidbody;
    protected float _speed;
    protected bool _grounded;

    protected Vector2 _input;
    protected bool _isRun;
    protected bool _isJump;
    protected bool _isPunch;
    protected CharacterController _controller;
    protected GameObject _mainCamera;

    protected Animator _animator;
    private float _animationBlend;
    private float jumpPower = 5;
    private int moveSpeed = 10;
    private int runSpeed = 20;

    protected virtual void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _charRigidbody = GetComponent<Rigidbody>();
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        _isJump = false;
        _isPunch = false;
    }

    protected virtual void Update()
    {
        // get input
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");
       

        _isRun = Input.GetKey(KeyCode.LeftShift);
       
        
        if(Input.GetKeyDown(KeyCode.Z) && !_isPunch && !_isJump && _input == Vector2.zero)
        {
            Punch();
            return;
        } else
        {
            _animator.SetBool("Punch", false);
            _animator.applyRootMotion = true;
        }

        Vector3 inputDir = new Vector3(_input.x, 0, _input.y).normalized;

        _isJump = Input.GetKey(KeyCode.Space);
        // 이동하는 방향으로 회전
        transform.LookAt(transform.position + inputDir);

        Jump();
      
        GroundCheck();
        
        if (!_isPunch)
        {
            if(!_isJump)
            {
                Move();
            }
        }
    }

    private void Move()
    {
        // set target speed.
        float targetSpeed = _isRun ? RunSpeed : MoveSpeed;
        if (_input == Vector2.zero) targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;


        float speedOffset = 0.1f;
        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // Lerping to speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * _speedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        Vector3 inputDirection = new Vector3(_input.x, 0.0f, _input.y).normalized;

        // again check for approximate 0 input.
        if (_input != Vector2.zero)
        {
            // target rotation is with respect to the camera's direction, not the character's forward direction!!
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;

            // rotate character in y axis towards the target rotation. SmoothDampAngle smooths the rotation.
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // move the player
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

        // animate
        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * _speedChangeRate);
        _animator.SetFloat("Speed", _animationBlend);
        _animator.SetFloat("MotionSpeed", 1.0f);
    }

    private void Jump()
    {
        

        if (_grounded)
        {
            
            _animator.SetBool("FreeFall", false);

            if (_isJump)
            {
                // v = 2gh. very popular physics equation
                _verticalVelocity = Mathf.Sqrt(JumpHeight * 2f * 9.8f);
                _animator.SetBool("Jump", true);
            }
            else
            {
                _animator.SetBool("Jump", false);
            }
        }
        else
        {
            // do not allow jump while in air.
            _isJump = false;
            _animator.SetBool("FreeFall", true);
        }

        // not using rigidbody.useGravity -> must apply gravity yourself!
        _verticalVelocity -= 9.8f * Time.deltaTime;
    }

    private void GroundCheck()
    {
        // set sphere position, with offset
        _grounded = Physics.CheckSphere(transform.position, _groundCheckRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        _animator.SetBool("Grounded", _grounded);
    }

    void Punch()
    {
        Debug.Log("Punch");
        _isPunch = true;
        _animator.applyRootMotion = false;
        _animator.SetBool("Punch", true);
        Invoke("ResetTrigger", 0.0f);
    }

    void ResetTrigger()
    {
        _isJump = false;
        _isPunch = false;
    }

}