using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour 
{
	private Animator m_anim;
	private bool m_doorOpen;

	private void Start()
	{
		m_doorOpen = false;
		m_anim = GetComponent<Animator> ();
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") 
		{
			OpenDoor ();
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Player") 
		{
			CloseDoor ();
		}
	}

	public void OpenDoor()
	{
		if (!m_doorOpen) 
		{
			m_doorOpen = true;
			DoorControl (m_doorOpen);
		}
	}

	public void CloseDoor()
	{
		if (m_doorOpen) 
		{
			m_doorOpen = false;
			DoorControl (m_doorOpen);
		}
	}

	private void DoorControl(bool isOpen)
	{
		m_anim.SetBool ("Open", isOpen);
	}
}
