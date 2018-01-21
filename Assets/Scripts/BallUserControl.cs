using System;
using UnityEngine;

public class BallUserControl : MonoBehaviour
{
	private Ball ball;
	private Vector3 move;
	private Transform cam;
	private Vector3 camForward;
	private bool jump;

	private void Awake ()
	{
		ball = GetComponent<Ball> ();

		if (Camera.main != null) {
			cam = Camera.main.transform;
		} 
	}

	private void Update ()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		jump = Input.GetButton ("Jump");

		if (cam != null) {
			camForward = Vector3.Scale (cam.forward, new Vector3 (1, 0, 1)).normalized;
			move = (v * camForward + h * cam.right).normalized;
		}
	}

	private void FixedUpdate ()
	{
		ball.Move (move, jump);
		jump = false;
	}
}

