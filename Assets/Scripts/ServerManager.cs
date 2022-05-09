using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManager : MonoBehaviourPunCallbacks
{
    private static ServerManager instance;
    public static ServerManager getServerManager() {
        return instance;
    }

    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            if(this != instance) {
                Destroy(this.gameObject);
            }
        }
    }
    private string nickName = "Player";
    private string gameVersion = "0.0.1";

    private void Start() {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
        bool joinedRoom = PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom() {
        if(PhotonNetwork.IsMasterClient) {
            PhotonNetwork.LoadLevel(1);
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        PhotonNetwork.CreateRoom("New Room", new RoomOptions());
    }

    public override void OnDisconnected(DisconnectCause cause) {
        Debug.Log("Disconnected from server" + cause.ToString());
        Application.Quit();
    }
}
