using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEssentials : MonoBehaviour 
{	
	public static LoadEssentials Instance = null;

	private void Awake()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);   
		}
		DontDestroyOnLoad(gameObject);

		if (GameSceneManager.Instance == null) {
			this.gameObject.AddComponent<GameSceneManager> ();
		}				
	}

	private void Start()
	{
		StartCoroutine (GameSceneManager.Instance.LoadAsyncSceneAdditive ("Player"));
	}
}
