using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PortalManager : MonoBehaviour
{
    // 쌍에 해당하는 포탈 오브젝트
    [SerializeField]
    private PortalManager _target;

    private Transform _spawnPoint;
    public Transform spawnPoint { get { return _spawnPoint; } }

    private CharacterController _controller;
    private bool _isActive;

    private void Start()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        _spawnPoint = transform.Find("SpawnPoint");
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
        Debug.Log(other.CompareTag("Player"));
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

        this.transform.DORotate(new Vector3(10, 10, 10), 1);
        _isActive = false;

        _controller.enabled = false;
        _controller.transform.position = _target.spawnPoint.position;
        _target.transform.DORotate(new Vector3(10, 10, 10), 2);
        _controller.enabled = true;
    }

}
