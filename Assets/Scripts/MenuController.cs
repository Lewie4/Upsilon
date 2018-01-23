using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public static MenuController Instance = null;

	[SerializeField] private Text m_versionText;
	[SerializeField] private EventSystem m_es;
	private GameObject m_selected;

	private void Awake()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);   
		}

		if (m_es == null) {
			m_es = GetComponent<EventSystem> ();
		}
	}

	private void Start () 
	{
		m_versionText.text = "v" + Application.version;
		m_selected = m_es.firstSelectedGameObject;
	}
	

	private void Update () 
	{
		if (m_es.currentSelectedGameObject != m_selected) {
			if (m_es.currentSelectedGameObject != null) {
				m_selected = m_es.currentSelectedGameObject;
			} else {
				m_es.SetSelectedGameObject(m_selected);
			}
		}
	}

	public void SetSelected(GameObject selected)
	{
		m_selected = selected;
		m_es.SetSelectedGameObject(selected);
	}

	public void LoadScene(string scene)
	{
		StartCoroutine(GameSceneManager.Instance.LoadAsyncScene (scene));
	}
}
