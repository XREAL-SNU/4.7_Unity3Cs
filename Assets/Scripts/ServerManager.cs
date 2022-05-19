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
        newPlayerNumber = Random.Range(0, 1000);
        string nickName = "Player " + newPlayerNumber.ToString();
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    private bool inputQ;
    private bool inputE;
    private string changeRoom = "";

    public void Update() {
        if(PhotonNetwork.InRoom) {
            if(PhotonNetwork.CurrentRoom.Name == "RoomQ") {
                inputE = Input.GetKey(KeyCode.E);
                if(inputE) {
                    changeRoom = "E";
                    PhotonNetwork.LeaveRoom();
                }
            }
            if(PhotonNetwork.CurrentRoom.Name == "RoomE") {
                inputQ = Input.GetKey(KeyCode.Q);
                if(inputQ) {
                    changeRoom = "Q";
                    PhotonNetwork.LeaveRoom();
                }
            }
        }
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
    }

    public override void OnJoinedLobby() {
        Debug.Log("CurrentLobby: " + PhotonNetwork.CurrentLobby.Name);
        if(changeRoom != "") {
            PhotonNetwork.JoinOrCreateRoom("Room" + changeRoom, new RoomOptions(), TypedLobby.Default);
        } else {
            PhotonNetwork.JoinOrCreateRoom("RoomQ", new RoomOptions(), TypedLobby.Default);
        }
    }

    public override void OnJoinedRoom() {
        changeRoom = "";
        Debug.Log("CurrentRoom: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("CurrentPlayerNumber: " + PhotonNetwork.CurrentRoom.PlayerCount);
        string SceneName = "Scene" + PhotonNetwork.CurrentRoom.Name.Substring(4, 1);
        if(PhotonNetwork.IsMasterClient) {
            Debug.Log("SceneLoaded: " + SceneName);
            PhotonNetwork.LoadLevel(SceneName);
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
        PhotonNetwork.Instantiate("Prefabs/Character", new Vector3(0, 20, newPlayerNumber/100), Quaternion.identity, 0);
    }
}
