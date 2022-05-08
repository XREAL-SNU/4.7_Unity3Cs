using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToNetwork : MonoBehaviourPunCallbacks
{
	public static ConnectToNetwork Instance = null;
	string nickname = "Your NickName";
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
		PhotonNetwork.ConnectUsingSettings(); // 마스터 서버와 연결시킵니다
	}

	public override void OnConnectedToMaster() // 마스터와 연결되었을 때
	{
		PhotonNetwork.JoinLobby(); // 마스터 서버 연결 후 로비에 접속
	}

	public override void OnJoinedLobby() // 로비에 접속했을 때
	{
		PhotonNetwork.JoinRandomRoom(); // 룸으로 접속하기
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("PlayerManager/JoinedRoom as " + PhotonNetwork.LocalPlayer.NickName);
		// load scene with PhotonNetwork.LoadLevel ONLY if I'm the only player in the room
		// we rely on AutomaticallySyncScene if there are other players in the room
		if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
		{
			// Must load level with PhotonNetwork.LoadLevel, not SceneManager.LoadScene
			PhotonNetwork.LoadLevel(SceneNameToLoad);
		}
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		// we create a new room.
		PhotonNetwork.CreateRoom(null, new RoomOptions());
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		// this will not get called on myself.
		Debug.Log($"Player {newPlayer.NickName} joined");
	}

	public override void OnDisconnected(DisconnectCause cause)
	{
		Debug.Log("Disconnected from server: " + cause.ToString());
		Application.Quit();
	}
}