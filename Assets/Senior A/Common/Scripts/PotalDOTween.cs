using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PotalDOTween : MonoBehaviour
{
    public Ease ease;
    //Sequence mySequence = DOTween.Sequence();
    bool isCheck;
    public Transform objectPivot;
    public Transform targetPotal;

    void Start()
    {

        //transform.DOJump(Vector3.forward * 10f, 3f, 5, 5f); �䳢�� �������
        //transform.DOScaleY(10, 5f).SetEase(ease);
        //transform.DOPunchPosition(Vector3.forward*5f, 4f); ��ġ�ϴ´���..?
        //transform.DOShakePosition(10f, 5f, 10, 90f, true, false); �ݷ��ϰ� ��鸮�±���..
        // transform.DOMoveZ(3f, 3f).SetEase(ease). �׳��� �ȵǳ�
        //Sequence sequence = DOTween.Sequence().Append() �������� ���������Ҷ��ϴ±���
        //transform.DOJump(Vector3.forward * 5f, 10f, 5, 5).SetLoops(-1, LoopType.Restart); �ݺ�
        //transform.DOLocalRotate(new Vector3(0, -360, 0), 1, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).OnPlay(()=>isCheck = false).OnUpdate(() =>
        //{
        //    if (Mathf.Abs(transform.localRotation.eulerAngles.y - 180) <= 5) ChangeColor();
        //});
        //DOTween.Sequence().Append(transform.DOLocalMoveY(0, 3f).From(5.5f, true, true).SetEase(Ease.InOutQuad)).Join(transform.DOLocalRotate(new Vector3(0,720,0), 2.8f, RotateMode.FastBeyond360).SetEase(Ease.OutQuart).SetDelay(2.8f)).Join(objectPivot.DOLocalRotate(new Vector3(0,0,70), .8f).SetEase(Ease.OutQuad)).Insert(3.5f, objectPivot.DOLocalRotate(new Vector3(0,0,0),1.5f).SetEase(Ease.OutQuad));
        
    }

    void ChangeColor()
    {
        if (isCheck) return;
        MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();
        for(int i = 0; i < mesh.Length; i++)
        {
            mesh[i].material.DOColor(Color.red, 3f);
        }

    }
    public bool EnterPlayer;
    void Update()
    {
        if(EnterPlayer)
        {
            EnterPlayer = false;
            DOTween.Sequence().Append(transform.DOJump(targetPotal.position, 3f, 10, 4).OnUpdate(() =>
            {
                if (transform.position == targetPotal.position)
                {
                    CharacterMovement.isGoing = false;
                   
                }
            })).SetDelay(2f);
        }
    }
}
