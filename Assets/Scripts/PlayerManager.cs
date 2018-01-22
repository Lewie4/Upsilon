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
	}

	public void RemovePlayer(PlayerHandler player)
	{
		m_players.Remove (player);
		m_playersSet--;
	}

	public static void SetPlayer(int id)
	{
		if (!m_players.Contains (new PlayerHandler { ControllerNumber = id })) {
			m_players [m_playersSet].SetControllerID (id);
			m_playersSet++;
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
