using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    public void Start() {
        GameManager.Instance().AddDoor(gameObject.name, this.GetComponent<Door>());
    }

    public void Warp() {
        DOTween.Sequence()
        .Append(transform.DORotate(new Vector3(-90f, 180f, 0f), 1f))
        .Append(transform.DORotate(new Vector3(0f, 180f, 0f), 1f));
    }
}
