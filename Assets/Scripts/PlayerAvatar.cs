using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PlayerAvatar : MonoBehaviour, IPunInstantiateMagicCallback
{
    private TextMeshPro nickName;
    private GameObject nickNameG;
    private PhotonView _view;
    public void Awake() {
        nickNameG = GameObject.Find("NickName").gameObject;
        nickName = nickNameG.GetComponent<TextMeshPro>();
        _view = this.GetComponent<PhotonView>();
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info) {
        info.Sender.TagObject = this.gameObject;
        CameraManager.getCameraManager().PlayerInstantiated();
        Debug.Log($"Player {info.Sender.NickName} is instantiated");
        if(_view.IsMine) {
            nickName.gameObject.SetActive(false);
            return;
        }
        nickName.text = info.Sender.NickName;
    }
}
