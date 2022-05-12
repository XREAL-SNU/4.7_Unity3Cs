using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkController : MonoBehaviourPunCallbacks
{
    string nickName;
    string gameVersion="0.0.1";
    void Init()
    {
        nickName="Player"+ Random.Range(1,1000);
        PhotonNetwork.NickName=nickName;
        PhotonNetwork.GameVersion=gameVersion;
    }

    private void Start() 
    {
        if(!PhotonNetwork.IsConnected)
            Init();
        PhotonNetwork.ConnectUsingSettings();//Master Server Connect

    }
    
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("Awake");
    }
    //Master Server
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN.");
        Debug.Log("Local Player" + PhotonNetwork.LocalPlayer.NickName);
        PhotonNetwork.JoinLobby();//Join Lobby
    }
    //Lobby
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Player Joined Lobby!!");
        Debug.Log("CurrentLobby Name :" + PhotonNetwork.CurrentLobby.Name); // lobby 정보호출
        Debug.Log("In Lobby" + PhotonNetwork.InLobby);

        if(SceneManager.GetActiveScene().name=="GameScene1")
            JoinRoom("GameScene1");
        else if(SceneManager.GetActiveScene().name=="GameScene2")
            JoinRoom("GameScene2");
        
    }
    public override void OnLeftLobby()
    {
        Debug.Log("Player Left Lobby!!");
        Debug.Log(PhotonNetwork.InLobby);
		Debug.Log(PhotonNetwork.IsConnected); // true
		PhotonNetwork.Disconnect(); // Master 연결 해제
    } 
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause.ToString());
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("InRoom"+ PhotonNetwork.InRoom);
        SpawnPlayer();

    }
    public override void OnLeftRoom()
	{
		Debug.Log("Left Room" +PhotonNetwork.InRoom);
        
        //PhotonNetwork.ConnectUsingSettings();//Master Server Connect
		if(SceneManager.GetActiveScene().name=="GameScene1")
        {
            SceneManager.LoadScene("GameScene2");
        }
        else if(SceneManager.GetActiveScene().name=="GameScene2")
        {
            SceneManager.LoadScene("GameScene1");
        }

	}


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New Player"+newPlayer.NickName);
        //LoadArena();
        //OnSceneLoaded();
    }

    //Scene Load
    public void LoadArena()//?
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        else
        {
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("GameScene");
        }        
    }
   /* void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(PhotonNetwork.OfflineMode || PhotonNetwork.InRoom)
        {
            InitializePlayer();
        }
    }
    */

    public void JoinRoom(string roomName)
    {
        RoomOptions roomOptions=new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);//방에 들어가기 
    }
    public void SpawnPlayer()
    {
        PhotonNetwork.Instantiate("SeniorA/Common/Character", new Vector3(0,0.87f,-19f), Quaternion.identity, 0);
    }



/*
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(PhotonNetwork.OfflineMode || PhotonNetwork.InRoom)
        {
            SpawnPlayer();
        }
    }
    public override void OnEnable()
    {
    	  // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    */
}
