using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
	public static PlayerManager Instance = null;

	[SerializeField] private GameObject m_playerPrefab;

	private static int m_controllersSetup = 5;

	private static List<PlayerHandler> m_players;
	private static int m_playersSet = 0;

	private void Awake()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);   
		}

		m_players = new List<PlayerHandler> ();
	}

	public void RegisterPlayer(PlayerHandler player)
	{
		m_players.Add (player);
		ScaleCameraToPlayers ();
	}

	public void RemovePlayer(PlayerHandler player)
	{
		m_players.Remove (player);
		m_playersSet--;
		ScaleCameraToPlayers ();
	}

	public static void SetPlayer(int id)
	{
		if (!m_players.Exists (x => x.ControllerNumber == id )) {
			m_players [m_playersSet].SetControllerID (id);
			m_playersSet++;
		}
	}

	private void ScaleCameraToPlayers()
	{
		switch (m_players.Count) {
		case 2:
			{
				m_players [0].m_cam.rect = new Rect (0.0f, 0.0f, 0.5f, 1.0f);
				m_players [1].m_cam.rect = new Rect (0.5f, 0.0f, 1.0f, 1.0f);
				break;
			}
		case 3: 
			{
				m_players [0].m_cam.rect = new Rect (0.0f, 0.5f, 1.0f, 1.0f);
				m_players [1].m_cam.rect = new Rect (0.0f, 0.0f, 0.5f, 0.5f);
				m_players [2].m_cam.rect = new Rect (0.5f, 0.0f, 1.0f, 0.5f);
				break;
			}
		case 4: 
			{
				m_players [0].m_cam.rect = new Rect (0.0f, 0.5f, 0.5f, 1.0f);
				m_players [1].m_cam.rect = new Rect (0.5f, 0.5f, 1.0f, 1.0f);
				m_players [2].m_cam.rect = new Rect (0.0f, 0.0f, 0.5f, 0.5f);
				m_players [3].m_cam.rect = new Rect (0.5f, 0.0f, 1.0f, 0.5f);
				break;
			}
		default:
			{
				m_players [0].m_cam.rect = new Rect (0.0f, 0.0f, 1.0f, 1.0f);
				break;
			}
		}
	}

	//TEMP
	private void Update()
	{
		if (m_players.Count >= m_playersSet) {
			for (int i = 0; i < m_controllersSetup; i++) {
				if (Input.GetButton (i + "Cancel")) {
					AddPlayer (i);
					SetPlayer (i);
				}
			}
		}
	}

	//TEMP 
	public void AddPlayer(int id)
	{
		if (m_players.Count == m_playersSet && (!m_players.Exists (x => x.ControllerNumber == id ))) {
			Vector3 playerPos = Vector3.zero;
			if(m_players.Count > 0)
			{
				playerPos = m_players [0].transform.GetComponentInChildren<Ball> ().gameObject.transform.position;
			}
			Vector3 spawnPos = new Vector3 (playerPos.x, playerPos.y + 5, playerPos.z);
			var newPlayer = Instantiate (m_playerPrefab, spawnPos, Quaternion.identity, this.transform);
			newPlayer.GetComponent<PlayerHandler> ().RegisterPlayer ();
		}
	}
}
