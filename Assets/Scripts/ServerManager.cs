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
    private string gameVersion = "0.0.1";
    private int newPlayerNumber;

    public void Start() {
        PhotonNetwork.AutomaticallySyncScene = true;
        newPlayerNumber = Random.Range(0, 10000);
        string nickName = "Player " + newPlayerNumber.ToString();
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
    }

    public override void OnJoinedLobby() {
        Debug.Log("CurrentLobby: " + PhotonNetwork.CurrentLobby.Name);
        bool joinedRoom = PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom() {
        Debug.Log("CurrentRoom: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("CurrentPlayerNumber: " + PhotonNetwork.CurrentRoom.PlayerCount);
        if(PhotonNetwork.IsMasterClient) {
            Debug.Log("LoadLevel");
            PhotonNetwork.LoadLevel("SampleScene");
            number ++;
        }
    }
    private int number = 1;

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        // Debug.Log(number + "ASD");
        // Debug.Log("LoadedScene");
        // Debug.Log("CompleteLoadScene");
        if(PhotonNetwork.OfflineMode || PhotonNetwork.InRoom) {
            // Debug.Log(number + "QWE");
            // Debug.Log("InitPlayerBefore");
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

    private void InitPlayer() {
        PhotonNetwork.Instantiate("Prefabs/Character", new Vector3(0, 3, newPlayerNumber/10), Quaternion.identity, 0);
    }
}
