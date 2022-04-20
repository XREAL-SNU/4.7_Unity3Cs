using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    protected bool _isMove;
    protected bool _isRun;
    protected bool _isJump;
    protected bool _isPunch;
    protected int emotion = 4;
    private bool canWarp = false;
    private bool wantWarp;
    private string startWarp;
    private string endWarp;
    private float emotionDuringTime = 10f;
    private float remainingEmotionDuringTime;
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
        _isPunch = Input.GetKey(KeyCode.Z);
        _isMove = !(_input.x == 0 && _input.y == 0);
        wantWarp = Input.GetKey(KeyCode.B);
        calculateRemainingEmotionTime();
        changeFaceEmotion();

        Jump();
        Punch();
        GroundCheck();
        Move();

        if (wantWarp && canWarp)
        {
            Warp();
        }
    }

    private void calculateRemainingEmotionTime()
    {
        if (emotionKeyDown())
        {
            remainingEmotionDuringTime = emotionDuringTime;
        }
        else
        {
            remainingEmotionDuringTime -= Time.deltaTime;
        }
        if (remainingEmotionDuringTime <= 0)
        {
            emotion = 4;
        }
    }

    private bool emotionKeyDown()
    {
        bool keyDown1 = Input.GetKey(KeyCode.Alpha1);
        bool keyDown2 = Input.GetKey(KeyCode.Alpha2);
        bool keyDown3 = Input.GetKey(KeyCode.Alpha3);
        bool keyDown4 = Input.GetKey(KeyCode.Alpha4);
        if (keyDown1)
        {
            emotion = 0;
        }
        else if (keyDown2)
        {
            emotion = 1;
        }
        else if (keyDown3)
        {
            emotion = 2;
        }
        else if (keyDown4)
        {
            emotion = 3;
        }
        if (keyDown1 || keyDown2 || keyDown3 || keyDown4)
        {
            return true;
        }
        else
        {
            return false;
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

    private void Punch()
    {
        if (_grounded)
        {
            if (_isPunch && !_isMove)
            {
                _animator.SetBool("Punch", true);
                _animator.applyRootMotion = false;
            }
            else
            {
                _animator.SetBool("Punch", false);
                _animator.applyRootMotion = true;
            }
        }
    }

    private void changeFaceEmotion()
    {
        string emotionName = "happy";
        if (emotion != 4)
        {
            emotionName = GameManager.Instance().getEmotion(emotion);
        }
        GameManager.Instance().getAvatarFaceControl().changeFace(emotionName);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Door1")
        {
            canWarp = true;
            startWarp = "Door1";
            endWarp = "Door2";
        }
        if (other.gameObject.name == "Door2")
        {
            canWarp = true;
            startWarp = "Door2";
            endWarp = "Door1";
        }
    }

    public void Warp()
    {
        canWarp = false;
        _controller.enabled = false;
        DOTween.Sequence()
        .Append(transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1f))
        .Append(transform.DOMove(GameManager.Instance().getDoor(endWarp).transform.position, 1f))
        .Append(transform.DOScale(new Vector3(1f, 1f, 1f), 1f))
        .AppendCallback(() =>
        {
            transform.position = GameManager.Instance().getDoor(endWarp).transform.position;
        })
        .OnComplete(() =>
        {
            _controller.enabled = true;
        });
        GameManager.Instance().getDoor(startWarp).Warp();
        GameManager.Instance().getDoor(endWarp).Warp();
    }
}