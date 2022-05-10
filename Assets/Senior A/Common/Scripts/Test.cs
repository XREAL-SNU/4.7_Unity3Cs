using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Test : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    Tweener floatTweener;
    Sequence showSequence, hideSequence;

    private void Start()
    {
        floatTweener = transform.DOLocalMoveY(0.5f, 1.5f).SetRelative().SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

        showSequence = DOTween.Sequence().SetAutoKill(false).Pause().AppendCallback(() => floatTweener.Play()).Join(transform.DOLocalRotate(new Vector3(0, 180, 0), 2).From().SetEase(curve)).Join(transform.DOScale(0, 2).From().SetEase(curve));

        hideSequence = DOTween.Sequence().SetAutoKill(false).Pause().Join(transform.DOLocalRotate(new Vector3(0, 180, 0), 1.5f).SetEase(Ease.InBack, 1.6f)).Join(transform.DOScale(0, 1.5f).SetEase(Ease.InBack, 1.6f)).AppendCallback(() => floatTweener.Pause());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hideSequence.Pause();
            showSequence.Restart();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            showSequence.Pause();
            hideSequence.Restart();
        }
    }
}
