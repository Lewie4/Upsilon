using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour 
{
	public class GameScene
	{
		public string m_sceneName;
		public bool m_isLoading;
		public bool m_isLoaded;
	}

	public static GameSceneManager Instance = null;

	private List<GameScene> m_scenes;

	private void Awake()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);   
		}
		DontDestroyOnLoad(gameObject);

		m_scenes = new List<GameScene>();

		m_scenes.Add(SetupGameScene(SceneManager.GetActiveScene().name, false, true));
	}

	public IEnumerator LoadAsyncSceneAdditive(string scene)
	{
		if (!m_scenes.Exists (x => x.m_sceneName == scene)) {
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (scene, LoadSceneMode.Additive);

			m_scenes.Add (SetupGameScene (scene, true, false));

			while (!asyncLoad.isDone) {
				yield return null;
			}

			var currentScene = m_scenes.Find(x => x.m_sceneName.Equals(scene)); 

			currentScene.m_isLoading = false;
			currentScene.m_isLoaded = true;
		}
	}

	private GameScene SetupGameScene(string name, bool isLoading, bool isLoaded)
	{
		GameScene gameScene = new GameScene ();
		gameScene.m_sceneName = name;
		gameScene.m_isLoading = isLoading;
		gameScene.m_isLoaded = isLoaded;

		return gameScene;
	}
}
