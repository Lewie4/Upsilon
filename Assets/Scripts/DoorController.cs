using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour 
{
	public enum ControlType
	{
		Button, 
		PressurePlate
	};

	[SerializeField] private DoorManager m_door;
	[SerializeField] private ControlType m_controlType;

	private Animator m_anim;
	private bool m_pressed;

	private void Start()
	{
		m_pressed = false;
		m_anim = GetComponent<Animator> ();

		m_door.RegisterController (this);
	}

	private void OnTriggerEnter(Collider col)
	{
		if (m_controlType == ControlType.Button) {
			ButtonPress (col);
		} else if (m_controlType == ControlType.PressurePlate) {
			PressurePlatePress (col);
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (m_controlType == ControlType.PressurePlate) 
		{
			PressurePlatePress (col);
		}
	}

	private void ButtonPress(Collider col)
	{
		if (col.gameObject.tag == "Player") 
		{
			m_pressed = !m_pressed;
			DoorControl ();
		}
	}

	private void PressurePlatePress(Collider col)
	{
		if (col.gameObject.tag == "Player") {	
			m_pressed = !m_pressed;
			DoorControl ();
		}
	}

	private void DoorControl()
	{
		m_anim.SetBool ("Pressed", m_pressed);
		if (m_door != null) 
		{
			m_door.ChangeState (m_pressed);
		}
	}

	public bool IsPressed()
	{
		return m_pressed;
	}
}
