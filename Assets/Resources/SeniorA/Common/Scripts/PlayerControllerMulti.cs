using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControllerMulti : CharacterControllerThirdPerson
{
    protected PhotonView _view;
    private void Awake() 
    {
        _view =GetComponent<PhotonView>();
    }
    protected override void Update() 
    {
        if(PhotonNetwork.OfflineMode)
        {
            base.Update();
            return;
        }
        if(_view.IsMine)
        {
            base.Update();
        }
    }
}
