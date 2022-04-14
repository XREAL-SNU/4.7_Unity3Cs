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
            StartCoroutine(Teleport());
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

    private IEnumerator Teleport()
    {

        this.transform.DORotate(new Vector3(50, 50, 50), 1);
        yield return new WaitForSeconds(1);
        _target.transform.DORotate(new Vector3(50, 50, 50), 1);
        _isActive = false;

        _controller.enabled = false;
        Vector3 newPosition = _target.spawnPoint.position;
        newPosition.y = (float) 0.0;
        _controller.transform.position = newPosition;

        _controller.transform.DORotate(new Vector3(10, 0, 0), 1);
       
        _controller.transform.rotation = Quaternion.Euler(new Vector3(-10, 0, 0));
        _controller.enabled = true;
    }

}
