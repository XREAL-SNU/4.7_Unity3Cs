using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal _target;

    private Transform _spawnPoint;
    public Transform spawnPoint { get { return _spawnPoint; } }

    public Transform _objectPivot;
    private CharacterController _controller;
    private bool _isActive;
    private Sequence _portalSequence;


    // Start is called before the first frame update
    void Start()
    {
        _spawnPoint = transform.Find("ObjectPivot/SpawnPoint");
        _objectPivot = transform.Find("ObjectPivot");

        _portalSequence = DOTween.Sequence().SetAutoKill(false)
            .Join(transform.DOLocalRotate(new Vector3(0, 360 * 30, 0), 5f, RotateMode.FastBeyond360).SetEase(Ease.InOutSine))
            .Join(_objectPivot.DOLocalRotate(new Vector3(0, 0, 80), 1f).SetEase(Ease.InSine))
            .Insert(3.5f, _objectPivot.DOLocalRotate(new Vector3(0, 0, 0), 1.5f).SetEase(Ease.OutSine))
            .Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isActive)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            ActivatePortal();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _controller = other.GetComponent<CharacterController>();
            _isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isActive = false;
    }
    private void ActivatePortal()
    {
        _isActive = false;
        _controller.enabled = false;

        _portalSequence.Rewind();
        _portalSequence.Play();

        CharacterAnimation characterAnimation = _controller.GetComponent<CharacterAnimation>();
        characterAnimation.Teleport(_controller,transform.position,_target.spawnPoint.position);
    }
}
