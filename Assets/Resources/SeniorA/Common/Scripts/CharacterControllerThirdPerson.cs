using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;
using Hashtable=ExitGames.Client.Photon.Hashtable;


public class CharacterControllerThirdPerson :  MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
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
    protected bool _isPunching;
    protected CharacterController _controller;
    protected GameObject _mainCamera;

    protected Animator _animator;
    private float _animationBlend;
    //DoTween
    //private GameObject _characterMesh;
    private Transform pivot;
    public enum ColorEnum{Red, Green, Blue, White};
    private int suitColor;
    protected virtual void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //_characterMesh=this.gameObject.transform.GetChild(4).gameObject;//_SpaceSuit
        pivot=this.transform;
        if(PhotonNetwork.LocalPlayer.CustomProperties[SUIT_COLOR_KEY] != null)
            SetColorProperty((ColorEnum)PhotonNetwork.LocalPlayer.CustomProperties[SUIT_COLOR_KEY]);
    }

    protected virtual void Update()
    {
        // get input
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");

        _isRun = Input.GetKey(KeyCode.LeftShift);
        _isJump = Input.GetKey(KeyCode.Space);
        _isPunching=Input.GetKey(KeyCode.Z);
        //Roll();
        Jump();
        GroundCheck();
        Move();
        Punch();

        //SetSuitColor
        if(Input.GetKeyDown(KeyCode.R))
            SetColorProperty(ColorEnum.Red);
        if(Input.GetKeyDown(KeyCode.G))
            SetColorProperty(ColorEnum.Green);
        if(Input.GetKeyDown(KeyCode.B))
            SetColorProperty(ColorEnum.Blue);
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
    /*
    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !_isJump && !_isRoll && !_isPunch)
        {
            _isRoll = true;
            _animator.SetTrigger("Roll");

            Invoke("ResetTrigger", 1f);
        }
    }

    void Punch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !_isJump && !_isRoll && !_isPunch)
        {
            _isRoll = true;
            _animator.SetTrigger("Punch");

            Invoke("ResetTrigger", 0.3f);
        }
    }

    */
    private void Punch()
    {
        if(_isPunching)
        {            
            _animator.SetBool("IsPunching", true);
            _animator.applyRootMotion=false;
        }
        else
        {
            _animator.SetBool("IsPunching", false);
            _animator.applyRootMotion=true;
        }
    }

    public void Teleport (CharacterController controller, Vector3 startPosition, Vector3 destination)
    {
        DOTween.Sequence()
            .Join(transform.DOMove(new Vector3(startPosition.x, transform.position.y, startPosition.z), 1f).SetEase(Ease.OutSine))
            .Join(pivot.DOLocalMoveX(0.8f, 0.8f)).SetEase(Ease.InOutSine)
            .Join(pivot.DOLocalMoveY(2, 1.5f)).SetEase(Ease.InExpo)
            .Join(pivot.DOLocalRotate(new Vector3(0, 360 * 10, 0), 5f, RotateMode.FastBeyond360).SetEase(Ease.InOutSine))
            .Insert(4.6f, pivot.DOLocalMoveY(1, 0.2f)).SetEase(Ease.OutQuart)
            .Insert(4.4f, pivot.DOLocalMoveX(0, 0.4f)).SetEase(Ease.OutSine)
            .Append(pivot.DOLocalMoveY(50, 0.2f)).SetEase(Ease.OutCubic)
            .AppendCallback(() =>
            {
                transform.position = destination;
                pivot.DOLocalMoveY(0, 0.2f).SetEase(Ease.OutCubic);
            }
            )
            .OnComplete(() => controller.enabled = true);
    }

    //Synchronize
    //이부분 왜??
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        info.Sender.TagObject=this.gameObject;
        Debug.Log($"Player {info.Sender.NickName}'s Avartar is Instantiated/t={info.SentServerTime}");
    }

  
    public void SetSuitColor(Player player, ColorEnum col) 
    {
        Debug.Log("SetSuitColor");
        MaterialPropertyBlock block=new MaterialPropertyBlock();
        block.SetColor("_Color", GetColor(col));

        GameObject playerGo = (GameObject)player.TagObject;
        Renderer suitRenderer = playerGo.transform.Find("Space_Suit/Tpose_/Man_Suit/Body").GetComponent<Renderer>();
        Debug.Log(suitRenderer);
        suitRenderer.SetPropertyBlock(block);
        //GameManager.Instance().suitColor=(int)col;
    }
    private Color GetColor(ColorEnum col)
    {
        if(col == ColorEnum.Red)
            return Color.red;
        if(col == ColorEnum.Green)
            return Color.green;
        if(col == ColorEnum.Blue)
            return Color.blue;
        return Color.white;
    }

    protected const string SUIT_COLOR_KEY="SuitColor";
    protected void SetColorProperty(ColorEnum col)
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable{{SUIT_COLOR_KEY, col}});
    }   
     
    public override void  OnPlayerPropertiesUpdate(Player player, Hashtable updatedProps)
    {
        if(updatedProps.ContainsKey(SUIT_COLOR_KEY))
        {
            SetSuitColor(player, (ColorEnum)updatedProps[SUIT_COLOR_KEY]);
        }
    }

}