#if UNITY_5 && (!UNITY_5_0 && !UNITY_5_1 && !UNITY_5_2 && ! UNITY_5_3) || UNITY_2017
#define UNITY_MIN_5_4
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerManager : Photon.PunBehaviour {

	[Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
	public static GameObject LocalPlayerInstance;
	[Tooltip("The Player's UI GameObject Prefab")]
	public GameObject PlayerUiPrefab;

	void Awake()
	{
		if ( photonView.isMine)
		{
			NetworkPlayerManager.LocalPlayerInstance = this.gameObject;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	void Start()
	{
		#if UNITY_MIN_5_4
		// Unity 5.4 has a new scene management. register a method to call CalledOnLevelWasLoaded.
		UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, loadingMode) =>
		{
			this.CalledOnLevelWasLoaded(scene.buildIndex);
		};
		#endif

		if (PlayerUiPrefab!=null)
		{
			GameObject _uiGo =  Instantiate(PlayerUiPrefab) as GameObject;
			_uiGo.SendMessage ("SetTarget", this, SendMessageOptions.RequireReceiver);
		} else {
			Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.",this);
		}
	}

	#if !UNITY_MIN_5_4
	/// <summary>See CalledOnLevelWasLoaded. Outdated in Unity 5.4.</summary>
	void OnLevelWasLoaded(int level)
	{
	this.CalledOnLevelWasLoaded(level);
	}
	#endif


	void CalledOnLevelWasLoaded(int level)
	{
		// check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
		if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
		{
			transform.position = new Vector3(0f, 5f, 0f);
		}
		GameObject _uiGo = Instantiate(this.PlayerUiPrefab) as GameObject;
		_uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
	}
}
