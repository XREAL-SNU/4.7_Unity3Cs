using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerManager : MonoBehaviourPunCallbacks
{
    private static ServerManager instance;
    public static ServerManager getServerManager() {
        return instance;
    }

    public override void OnEnable() {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public override void OnDisable() {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            if(this != instance) {
                Destroy(this.gameObject);
            }
        }
    }
    private static int playerNumber = 1;
    private string nickName = "Player";
    private string gameVersion = "0.0.1";

    public void Start() {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
        Debug.Log("JoinedLobby");
        bool joinedRoom = PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom() {
        Debug.Log("JoinedRoom");
        if(PhotonNetwork.IsMasterClient) {
            PhotonNetwork.LoadLevel("SampleScene");
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if(PhotonNetwork.OfflineMode || PhotonNetwork.InRoom) {
            InitPlayer();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        PhotonNetwork.CreateRoom("New Room", new RoomOptions());
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        Debug.Log($"Player {newPlayer.NickName} joined");
    }

    public override void OnDisconnected(DisconnectCause cause) {
        Debug.Log("Disconnected from server" + cause.ToString());
        Application.Quit();
    }

    private int player = -23;

    private void InitPlayer() {
        PhotonNetwork.Instantiate("Prefabs/Character", new Vector3(0, 3, player), Quaternion.identity, 0);
        player++;
    }
}
