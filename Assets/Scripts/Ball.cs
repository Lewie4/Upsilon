using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
	[SerializeField] private float m_movePower = 5;
	[SerializeField] private bool m_useTorque = true;
	[SerializeField] private float m_maxAngularVelocity = 25;
	[SerializeField] private float m_jumpPower = 2;

	private const float m_groundRayLength = 1f;	// The length of the ray to check if the ball is grounded.
	private Rigidbody m_Rigidbody;


	private void Start ()
	{
		m_Rigidbody = GetComponent<Rigidbody> ();
		// Set the maximum angular velocity.
		GetComponent<Rigidbody> ().maxAngularVelocity = m_maxAngularVelocity;
	}


	public void Move (Vector3 moveDirection, bool jump)
	{
		// If using torque to rotate the ball...
		if (m_useTorque) {
			// ... add torque around the axis defined by the move direction.
			m_Rigidbody.AddTorque (new Vector3 (moveDirection.z, 0, -moveDirection.x) * m_movePower);
		} else {
			// Otherwise add force in the move direction.
			m_Rigidbody.AddForce (moveDirection * m_movePower);
		}

		// If on the ground and jump is pressed...
		if (Physics.Raycast (transform.position, -Vector3.up, m_groundRayLength) && jump) {
			// ... add force in upwards.
			m_Rigidbody.AddForce (Vector3.up * m_jumpPower, ForceMode.Impulse);
		}
	}
}

