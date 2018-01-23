using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEssentials : MonoBehaviour 
{	
	[SerializeField] private bool m_takeActiveSceneOnLoad = false;
	[SerializeField] private List<string> m_requiredScenes;

	private void Start()
	{
		GameSceneManager.Instance.SetupEssentials (this.gameObject.scene.name, this);
	}

	public bool TakeActiveSceneOnLoad()
	{
		return m_takeActiveSceneOnLoad;
	}

	public List<string> RequiredScenes()
	{
		return m_requiredScenes;
	}
}
