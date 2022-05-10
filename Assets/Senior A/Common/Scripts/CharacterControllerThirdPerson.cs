using System;
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
    protected float _speed;
    protected bool _grounded;

    protected Vector2 _input;
    protected bool _isRun;
    protected bool _isJump;
    protected bool _isRoll;
    protected bool _isPunch;
    protected CharacterController _controller;
    protected GameObject _mainCamera;

    protected Animator _animator;
    private float _animationBlend;

    protected virtual void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    protected virtual void Update()
    {
        // get input
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");

        _isRun = Input.GetKey(KeyCode.LeftShift);
        _isJump = Input.GetKey(KeyCode.Space);

        Roll();
        Jump();
        GroundCheck();
        Move();

        Punch();
    }

    private void Punch()
    {
    
    }

    private void Roll()
    {
 
    }

    private void Move()
    {
        
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
    

    
}