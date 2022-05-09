using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour, IPunInstantiateMagicCallback
{
    public void OnPhotonInstantiate(PhotonMessageInfo info) {
        info.Sender.TagObject = this.gameObject;
        Debug.Log($"Player {info.Sender.NickName} is instantiated");
    }
}
