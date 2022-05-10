using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ConnectToNetwork : MonoBehaviourPunCallbacks
{
	public static ConnectToNetwork Instance = null;
	string nickname = "Jin woo";
	string gameVersion = "0.0.1";
	const string SceneNameToLoad = "SampleScene";
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
	}

	private void Start()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
		PhotonNetwork.NickName = nickname;
		PhotonNetwork.GameVersion = gameVersion;
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster() 
	{
		PhotonNetwork.JoinLobby(); 
	}

	public override void OnJoinedLobby() 
	{
		PhotonNetwork.JoinRandomRoom();
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("PlayerManager/JoinedRoom as " + PhotonNetwork.LocalPlayer.NickName);

		//PhotonNetwork.LoadLevel(SceneNameToLoad);
		InitializePlayer();
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		PhotonNetwork.CreateRoom(null, new RoomOptions()); //default값인가?
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		Debug.Log($"Player {newPlayer.NickName} joined");
		//InitializePlayer();
	}

	public override void OnDisconnected(DisconnectCause cause)
	{
		//Debug.Log("Disconnected from server: " + cause.ToString());
		Application.Quit();
	}
	void InitializePlayer()
	{
        //Resources.Load는 Resources폴더 안에서 주어진 경로의 prefab을 찾는데 이용

       // var FirstPersonCam = (GameObject)Resources.Load("PhotonPrefabs/FirstPersonCam");
       // var ThirdPersonCam = (GameObject)Resources.Load("PhotonPrefabs/ThirdPersonCam");
        //var cam1 = Instantiate(FirstPersonCam, Vector3.zero, Quaternion.identity);
        //var cam2 = Instantiate(ThirdPersonCam, Vector3.zero, Quaternion.identity);
        //cam1.name = "PlayerFollowCamera";
       // cam2.name = "ThirdPersonCam";
        var player = PhotonNetwork.Instantiate("PhotonPrefabs/Character", Vector3.zero, Quaternion.identity);
        if (player != null)
        {
           //cam1.GetComponent<CinemachineVirtualCamera>().Follow = player.transform.GetChild(5).Find("FollowTarget");
			//    cam2.GetComponent<CinemachineVirtualCamera>().Follow = player.transform.Find("FollowTarget");
		}
    }
}