using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour 
{
	[SerializeField] private DoorController m_doorController;

	private Animator m_anim;
	private bool m_buttonPressed;

	private void Start()
	{
		m_buttonPressed = false;
		m_anim = GetComponent<Animator> ();
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") 
		{
			m_buttonPressed = !m_buttonPressed;
			ButtonControl (m_buttonPressed);
		}
	}

	private void ButtonControl(bool isDown)
	{
		m_anim.SetBool ("Pressed", m_buttonPressed);
		DoorControl (m_buttonPressed);
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
