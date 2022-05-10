using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;

public class AvatarFaceControl : MonoBehaviour
{
    [SerializeField] Material _avatarFace;         // Material "Face"
    [SerializeField] Texture _defaultTexture;      // PNG Image "/UI Assets/Face Images/emoticon/happy.png"

    bool _crRunning = false;
    IEnumerator _coroutine;

    void Start()
    {
        _avatarFace.SetTexture("_MainTex", _defaultTexture);
    }

    public void ChangeFace(int faceIndex)
    {
        _avatarFace.SetTexture("_MainTex", QuickSlotManager.s_faceTextures[faceIndex]);
    }

    IEnumerator ShowFaceCoroutine(int index)
    {
        _crRunning = true;
        // 표정 변경
        ChangeFace(index);

        // 10초 대기
        yield return new WaitForSeconds(10f);

        // 표정을 다시 default로 바꾸기 (Start에서 fid를 받아오는 방법도 가능)
        ChangeFace(11);

        _crRunning = false;
    }

    private void Update()
    {
        //if (!PhotonView.Get(this).IsMine) return;
        if (Input.GetKeyUp(KeyCode.Alpha1)) PhotonView.Get(this).RPC("ShowFace", RpcTarget.All, (byte)0);
        if (Input.GetKeyUp(KeyCode.Alpha2)) PhotonView.Get(this).RPC("ShowFace", RpcTarget.All, (byte)1);
        if (Input.GetKeyUp(KeyCode.Alpha3)) PhotonView.Get(this).RPC("ShowFace", RpcTarget.All, (byte)2);
        if (Input.GetKeyUp(KeyCode.Alpha4)) PhotonView.Get(this).RPC("ShowFace", RpcTarget.All, (byte)3);
    }

    [PunRPC]
    void ShowFace(byte index)
    {
        if (_crRunning && _coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = ShowFaceCoroutine(index);
        StartCoroutine(_coroutine);
    }
}