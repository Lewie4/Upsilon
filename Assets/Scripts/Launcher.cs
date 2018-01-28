﻿using UnityEngine;

public class Launcher : Photon.PunBehaviour
{
	public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;
	[Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
	public byte MaxPlayersPerRoom = 4;
	[Tooltip("The Ui Panel to let the user enter name, connect and play")]
	public GameObject controlPanel;
	[Tooltip("The UI Label to inform the user that the connection is in progress")]
	public GameObject progressLabel;

	string _gameVersion = "1";
	bool isConnecting;

	void Awake ()
	{
		PhotonNetwork.autoJoinLobby = false;
		PhotonNetwork.automaticallySyncScene = true;
		PhotonNetwork.logLevel = Loglevel;
	}

	void Start ()
	{
		progressLabel.SetActive(false);
		controlPanel.SetActive(true);
	}

	public void Connect ()
	{
		isConnecting = true;
		progressLabel.SetActive(true);
		controlPanel.SetActive(false);
		if (PhotonNetwork.connected) {
			PhotonNetwork.JoinRandomRoom ();
		} else {
			PhotonNetwork.ConnectUsingSettings (_gameVersion);
		}
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
		if (isConnecting) {
			PhotonNetwork.JoinRandomRoom ();
		}
	}


	public override void OnDisconnectedFromPhoton()
	{
		Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");    
		progressLabel.SetActive(false);
		controlPanel.SetActive(true);
	}

	public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
	{
		Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
		PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
		if (PhotonNetwork.room.PlayerCount == 1)
		{
			Debug.Log("We load the 'Room for 1' ");
			PhotonNetwork.LoadLevel("Room for 1");
		}
	}

}