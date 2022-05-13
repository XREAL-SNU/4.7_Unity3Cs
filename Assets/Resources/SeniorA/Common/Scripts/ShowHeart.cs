using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using DG.Tweening;

public class ShowHeart : MonoBehaviour
{
    bool isHeart=false;
    bool isMoving=false;
    public GameObject heartPrefab;
    public GameObject heart;
    private float _targetY = 3;
    private float _tweeningDuration=2;

    private Sequence heartMoveSequence;
    private void Start() 
    {
        heartPrefab=Resources.Load<GameObject>("Heart");
        //heartMoveSequence=DOTween.Sequence()
        //    .Append(transform.DOLocalMoveY(_targetY, _tweeningDuration).SetEase(Ease.Linear));
    }
    // Update is called once per frame
    void Update()
    {
        if(!PhotonView.Get(this).IsMine) return;

        if(Input.GetKeyDown(KeyCode.H))
        {
            PhotonView.Get(this).RPC("SpawnHeart", RpcTarget.All);
        }
        if(Input.GetKeyUp(KeyCode.H))
        {
            PhotonView.Get(this).RPC("DestroyHeart", RpcTarget.All);
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            isMoving=true;
            PhotonView.Get(this).RPC("SpawnHeart", RpcTarget.All);
        }
        if(Input.GetKeyUp(KeyCode.J))
        {
            PhotonView.Get(this).RPC("DestroyHeart", RpcTarget.All);
        }
    }

    [PunRPC]
    void SpawnHeart()
    {
        if(!isHeart)
        {
            //HeartSpawn
            heart=Instantiate(heartPrefab);
            heart.transform.SetParent(this.gameObject.transform.Find("HeartPosition"));
            heart.transform.localPosition=new Vector3(0,0,0);
            isHeart=true;
            if(isMoving)
            {
                //heartMoveSequence.Rewind();
                //heartMoveSequence.Play();
            }
        }
    }

    

    [PunRPC]
    void DestroyHeart()
    {
        if(heart)
        {
            Destroy(heart);
            isHeart=false;
            isMoving=false;
        }     
    }
}
