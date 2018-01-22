using System;
using UnityEngine;

public class BallUserControl : MonoBehaviour
{
	[SerializeField] private PlayerHandler m_inputHandler;
	[SerializeField] private Transform m_cam;
	
	private Ball m_ball;
	private Vector3 m_move;
	private Vector3 m_camForward;
	private bool m_jump;

	private void Awake ()
	{
		m_ball = GetComponent<Ball> ();

		if (m_cam == null) {
			Debug.LogError ("m_cam not set on " + gameObject.name);
		} 

		if (m_inputHandler == null) {
			m_inputHandler = transform.GetComponentInParent<PlayerHandler> ();
		}
	}

	private void Update ()
	{
		if (m_inputHandler.ControllerNumber != -1) {
			float h = Input.GetAxis (m_inputHandler.GetControllerID () + "Horizontal");
			float v = Input.GetAxis (m_inputHandler.GetControllerID () + "Vertical");
			m_jump = Input.GetButton (m_inputHandler.GetControllerID () + "Jump");

			if (m_cam != null) {
				m_camForward = Vector3.Scale (m_cam.forward, new Vector3 (1, 0, 1)).normalized;
				m_move = (v * m_camForward + h * m_cam.right).normalized;
			}
		}
	}

	private void FixedUpdate ()
	{
		m_ball.Move (m_move, m_jump);
		m_jump = false;
	}
}

