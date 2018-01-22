using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
	public static PlayerManager Instance = null;

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

		foreach (var joystick in Input.GetJoystickNames ()) {
			Debug.Log (joystick);
		}
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
		if (!m_players.Contains (new PlayerHandler { ControllerNumber = id })) {
			m_players [m_playersSet].SetControllerID (id);
			m_playersSet++;
		}
	}

	private void ScaleCameraToPlayers()
	{
		switch (m_players.Count) {
		case 1:
			{
				m_players [0].m_cam.rect = new Rect (0, 0, 1, 1);
				break;
			}
		case 2:
			{
				m_players [0].m_cam.rect = new Rect (0, 0, 0.5f, 1);
				m_players [1].m_cam.rect = new Rect (0.5f, 0, 1, 1);
				break;
			}
		}
	}

	//TEMP
	private void Update()
	{
		if (m_players.Count != m_playersSet) {
			for (int i = 0; i < m_controllersSetup; i++) {
				if (Input.GetButton (i + "Jump")) {
					SetPlayer (i);
				}
			}
		}
	}
}
