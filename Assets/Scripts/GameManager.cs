using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Photon.PunBehaviour
{
	public static GameManager Instance = null;

	[Tooltip("The prefab to use for representing the player")]
	public GameObject playerPrefab;

	private void Start()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);   
		}

		if (playerPrefab == null) 
		{
			Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
		} 
		else
		{
			Debug.Log("We are Instantiating LocalPlayer from "+Application.loadedLevelName);
			// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
			if (NetworkPlayerManager.LocalPlayerInstance==null)
			{
				Debug.Log("We are Instantiating LocalPlayer from "+Application.loadedLevelName);
				// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
				SpawnPlayer();
			}
			else
			{
				Debug.Log("Ignoring scene load for "+Application.loadedLevelName);
			}
		}
	}

	public GameObject SpawnPlayer()
	{
		return PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,5f,0f), Quaternion.identity, 0);
	}

	public override void OnLeftRoom ()
	{
		SceneManager.LoadScene (0);
	}

	public void LeaveRoom ()
	{
		PhotonNetwork.LeaveRoom ();
	}


	void LoadArena()
	{
		if (!PhotonNetwork.isMasterClient) 
		{
			Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
		}
		Debug.Log("PhotonNetwork : Loading Level : 1" + PhotonNetwork.room.PlayerCount);
		PhotonNetwork.LoadLevel("Room for 1");
	}

	public override void OnPhotonPlayerConnected(PhotonPlayer other)
	{
		Debug.Log("OnPhotonPlayerConnected() " + other.NickName); // not seen if you're the player connecting


		if (PhotonNetwork.isMasterClient) 
		{
			Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


			LoadArena();
		}
	}


	public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
	{
		Debug.Log("OnPhotonPlayerDisconnected() " + other.NickName); // seen when other disconnects


		if (PhotonNetwork.isMasterClient) 
		{
			Debug.Log("OnPhotonPlayerDisonnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


			LoadArena();
		}
	}
}
