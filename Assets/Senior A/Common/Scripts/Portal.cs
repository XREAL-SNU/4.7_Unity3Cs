using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Portal : MonoBehaviour
{
    [SerializeField]
    private Portal _target;
    private Material _material;
    private Transform _spawnPoint;
    public Transform spawnPoint { get { return _spawnPoint; } }


    private CharacterController _controller;
    private bool _isActive;
    private Sequence _portalSequence;

    private void Start()
    {
        _spawnPoint = transform.Find("SpawnPoint"); 

        _portalSequence = DOTween.Sequence()
            .SetAutoKill(false)
            .Join(transform.DOScale(new Vector3(3, 3, 3),2f))
            .Join(_material.DOFade(0,3f))
            .Append(transform.DOScale(new Vector3(0,0,0), 2f))

            .Join(transform.DOScale(new Vector3(1,1,1), 3f))
            .Pause();
    }

    private void Update()
    {
        if (!_isActive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ActivatePortal();
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

    private void ActivatePortal()
    {
        _isActive = false;
        _controller.enabled = false;

        _portalSequence.Rewind();
        _portalSequence.Play();

        CharacterAnimation characterAnimation = _controller.GetComponent<CharacterAnimation>();
        characterAnimation.Teleport(_controller, transform.position, _target.spawnPoint.position);
    }
}