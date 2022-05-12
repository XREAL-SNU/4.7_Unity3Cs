using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;


public class PlayerControllerMulti : CharacterControllerThirdPerson
{
    protected PhotonView _view;
    protected NetworkController networkController;
    private void Awake() 
    {
        _view =GetComponent<PhotonView>();
        networkController=GameObject.FindGameObjectWithTag("Networking").GetComponent<NetworkController>();
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
            if(Input.GetKeyDown(KeyCode.Q))
                ChangeScene(1);
            if(Input.GetKeyDown(KeyCode.E))
                ChangeScene(2);
        }
    }

    protected void ChangeScene(int num)
    {
        if(SceneManager.GetActiveScene().name != "GameScene"+num)
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}
