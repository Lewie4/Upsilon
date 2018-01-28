using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : Photon.MonoBehaviour {

	public Camera m_cam;

	public int ControllerNumber
	{
		get{
			return m_controllerNumber;
		}
		set{
			m_controllerNumber = value;
		}
	}

	//Control setup
	//-1 	- Not set
	//0 	- Keyboard + Mouse
	//1 	- Joystick 1
	//2 	- Joystick 2
	//3 	- Joystick 3
	//4 	- Joystick 4
	private int m_controllerNumber = -1;
	private string m_controllerID = "-1";
	private bool m_registered = false;

	private void Awake()
	{
		if (m_cam == null) {
			Debug.LogError ("SET A CAMERA IN THE PLAYERHANDLER!!!");
			m_cam = Camera.main;
		}
	}

	private void Start()
	{
		RegisterPlayer ();
	}

	public void RegisterPlayer()
	{
		if (PlayerManager.Instance != null && photonView.isMine) {
			if (!m_registered) {
				PlayerManager.Instance.RegisterPlayer (this);
				m_registered = true;
			}
		}
	}

	private void OnDestroy()
	{
		if (PlayerManager.Instance != null && photonView.isMine) {
			if (m_registered) {
				PlayerManager.Instance.RemovePlayer (this);
				m_registered = false;
			}
		}
	}

	public void SetControllerID(int id)
	{
		m_controllerNumber = id;
		m_controllerID = id.ToString ();
	}

	public string GetControllerID()
	{
		return m_controllerID;
	}
}
