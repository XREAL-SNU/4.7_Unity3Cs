using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerThirdPersonMulti : CharacterControllerThirdPerson
{
    PhotonView _view;
    protected override void Start() {
        base.Start();
        _view = this.GetComponent<PhotonView>();
    }
    protected override void Update() {
        if(_view.IsMine) {
            base.Update();
        }
    }
}


