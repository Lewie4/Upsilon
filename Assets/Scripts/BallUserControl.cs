using System;
using UnityEngine;

public class BallUserControl : MonoBehaviour
{
	private Ball m_ball;
	private Vector3 m_move;
	private Transform m_cam;
	private Vector3 m_camForward;
	private bool m_jump;

	private void Awake ()
	{
		m_ball = GetComponent<Ball> ();

		if (Camera.main != null) {
			m_cam = Camera.main.transform;
		} 
	}

	private void Update ()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		m_jump = Input.GetButton ("Jump");

		if (m_cam != null) {
			m_camForward = Vector3.Scale (m_cam.forward, new Vector3 (1, 0, 1)).normalized;
			m_move = (v * m_camForward + h * m_cam.right).normalized;
		}
	}

	private void FixedUpdate ()
	{
		m_ball.Move (m_move, m_jump);
		m_jump = false;
	}
}

