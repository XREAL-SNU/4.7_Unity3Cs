using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CharacterAnimation : MonoBehaviour
{

    private Transform _player;

    private void Start()
    {
        _player = transform;
    }

    public void Teleport(CharacterController controller, Vector3 startPosition, Vector3 destination)
    {
        DOTween.Sequence()
            .Join(transform.DOLocalRotate(new Vector3(0, 360 * 30, 0), 4f, RotateMode.FastBeyond360).SetEase(Ease.InOutSine))
            .AppendCallback(() =>
            {
                transform.position = destination;
                _player.DOLocalMoveY(0, 0.2f).SetEase(Ease.OutCubic);
            }
            )
            .OnComplete(() => controller.enabled = true)
            .Append(transform.DOMoveX(1, 0.1f).SetEase(Ease.Linear));

    }
}