using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour {

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

	private void Start()
	{
		PlayerManager.Instance.RegisterPlayer (this);
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
