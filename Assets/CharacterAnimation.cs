using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterAnimation : MonoBehaviour
{
    private Transform _objectPivot;
    private Transform _spaceSuit;

    // Start is called before the first frame update
    void Start()
    {
        _objectPivot = transform.Find("ObjectPivot");
        _spaceSuit = transform.Find("ObjectPivot/Space_Suit");
    }

    public void Teleport(CharacterController controller, Vector3 startPosition, Vector3 destination)
    {
        DOTween.Sequence()
            .Join(transform.DOMove(new Vector3(startPosition.x, transform.position.y, startPosition.z), 1f).SetEase(Ease.OutSine))
            .Join(_spaceSuit.DOLocalMoveX(0.8f, 0.8f)).SetEase(Ease.InOutSine)
            .Join(_spaceSuit.DOLocalMoveY(2, 1.5f)).SetEase(Ease.InExpo)
            .Join(_objectPivot.DOLocalRotate(new Vector3(0, 360 * 10, 0), 5f, RotateMode.FastBeyond360).SetEase(Ease.InOutSine))
            .Insert(4.6f, _spaceSuit.DOLocalMoveY(1, 0.2f)).SetEase(Ease.OutQuart)
            .Insert(4.4f, _spaceSuit.DOLocalMoveX(0, 0.4f)).SetEase(Ease.OutSine)
            .Append(_spaceSuit.DOLocalMoveY(50, 0.2f)).SetEase(Ease.OutCubic)
            .AppendCallback(() =>
            {
                transform.position = destination;
                _spaceSuit.DOLocalMoveY(0, 0.2f).SetEase(Ease.OutCubic);
            }
            )
            .OnComplete(() => controller.enabled = true);
    }
}
