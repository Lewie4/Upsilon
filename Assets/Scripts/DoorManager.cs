using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour 
{
	private Animator m_anim;
	private bool m_doorOpen;

	private List<DoorController> m_doorControllers;

	private void Awake()
	{
		m_doorControllers = new List<DoorController> ();
	}

	private void Start()
	{
		m_doorOpen = false;
		m_anim = GetComponent<Animator> ();

	}

	public void RegisterController(DoorController controller)
	{
		m_doorControllers.Add (controller);
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") 
		{
			ChangeState (true);
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Player") 
		{
			ChangeState (false);
		}
	}

	public void ChangeState(bool open)
	{
		if (m_doorOpen != open) 
		{
			if (open || (!AnyControlsPressed ())) {
				m_doorOpen = !m_doorOpen;
				DoorControl (m_doorOpen);
			}
		}
	}

	private bool AnyControlsPressed()
	{
		foreach (var controller in m_doorControllers) {
			if (controller.IsPressed ()) {
				return true;
			}
		}
		return false;
	}

	private void DoorControl(bool isOpen)
	{
		m_anim.SetBool ("Open", isOpen);
	}
}
