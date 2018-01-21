using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour 
{
	[SerializeField] private DoorController m_doorController;

	private Animator m_anim;
	private bool m_plateDown;

	private void Start()
	{
		m_plateDown = false;
		m_anim = GetComponent<Animator> ();
	}

	private void OnTriggerEnter(Collider col)
	{
		if (!m_plateDown && col.gameObject.tag == "Player") 
		{
			m_plateDown = true;
			PlateControl (m_plateDown);
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (m_plateDown && col.gameObject.tag == "Player") 
		{
			m_plateDown = false;
			PlateControl (m_plateDown);
		}
	}

	private void PlateControl(bool isDown)
	{
		m_anim.SetBool ("Down", m_plateDown);
		DoorControl (m_plateDown);
	}

	private void DoorControl(bool isDown)
	{
		if (m_doorController != null) 
		{
			if (isDown) {
				m_doorController.OpenDoor ();
			} else {
				m_doorController.CloseDoor ();
			}
		}
	}
}
