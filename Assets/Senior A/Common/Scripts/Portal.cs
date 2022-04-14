using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Portal : MonoBehaviour
{
    // 쌍에 해당하는 포탈 오브젝트
    [SerializeField]
    private Portal _target;

    private Transform _spawnPoint;
    public Transform spawnPoint { get { return _spawnPoint; } }
    private Transform _objectPivot;

    private CharacterController _controller;
    private bool _isActive;
    private Sequence _portalSequence;

    private void Start()
    {
        _spawnPoint = transform.Find("SpawnPoint");
        /*_portalSequence = DOTween.Sequence()
           .Join(transform.DOLocalRotate(new Vector3(0, 360 * 30, 0), 5f, RotateMode.FastBeyond360).SetEase(Ease.InOutSine))
           .Join(_objectPivot.DOLocalRotate(new Vector3(0, 0, 80), 1f).SetEase(Ease.InSine))
           .Insert(3.5f, _objectPivot.DOLocalRotate(new Vector3(0, 0, 0), 1.5f).SetEase(Ease.OutSine));*/
    }

    private void Update()
    {
        if (!_isActive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Teleport();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _controller = other.GetComponent<CharacterController>();
            _isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isActive = false;
    }

    private void Teleport()
    {
        _isActive = false;

        _controller.enabled = false;
        _controller.transform.position = _target.spawnPoint.position;
        _controller.enabled = true;
    }
}
