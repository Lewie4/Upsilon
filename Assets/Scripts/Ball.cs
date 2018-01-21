using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
	[SerializeField] private float m_movePower = 5;
	[SerializeField] private bool m_useTorque = true;
	[SerializeField] private float m_maxAngularVelocity = 7;
	[SerializeField] private float m_jumpPower = 1;

	private const float m_groundRayLength = 1f;	// The length of the ray to check if the ball is grounded.
	private Rigidbody m_Rigidbody;

	private void Start ()
	{
		m_Rigidbody = GetComponent<Rigidbody> ();
		GetComponent<Rigidbody> ().maxAngularVelocity = m_maxAngularVelocity;
	}

	public void Move (Vector3 moveDirection, bool jump)
	{
		if (m_useTorque) {
			m_Rigidbody.AddTorque (new Vector3 (moveDirection.z, 0, -moveDirection.x) * m_movePower);
		} else {
			m_Rigidbody.AddForce (moveDirection * m_movePower);
		}
			
		if (Physics.Raycast (transform.position, -Vector3.up, m_groundRayLength) && jump) {
			m_Rigidbody.AddForce (Vector3.up * m_jumpPower, ForceMode.Impulse);
		}
	}
}

