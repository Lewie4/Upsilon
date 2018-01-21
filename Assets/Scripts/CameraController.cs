using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject m_player;
	public float m_xSpeed = 2.0f;
	public float m_ySpeed = 2.0f;
	public float m_yMinLimit = -90f;
	public float m_yMaxLimit = 90f;
	public float m_smoothTime = 2f;

	private Vector3 m_offset;
	private float m_rotationYAxis = 0.0f;
	private float m_rotationXAxis = 0.0f;
	private float m_velocityX = 0.0f;
	private float m_velocityY = 0.0f;

	private void Start()
	{
		m_offset = transform.position - m_player.transform.position;

		Vector3 angles = transform.eulerAngles;
		m_rotationYAxis = angles.y;
		m_rotationXAxis = angles.x;
	}

	private void LateUpdate()
	{
		if (m_player) {
			transform.position = m_player.transform.position + m_offset;

			m_velocityX += m_xSpeed * Input.GetAxis ("Mouse X") * 0.02f;
			m_velocityY += m_ySpeed * Input.GetAxis ("Mouse Y") * 0.02f;

			m_rotationYAxis += m_velocityX;
			m_rotationXAxis -= m_velocityY;
			m_rotationXAxis = ClampAngle (m_rotationXAxis, m_yMinLimit, m_yMaxLimit);
			Quaternion rotation = Quaternion.Euler (m_rotationXAxis, m_rotationYAxis, 0);

			transform.rotation = rotation;

			m_velocityX = Mathf.Lerp (m_velocityX, 0, Time.deltaTime * m_smoothTime);
			m_velocityY = Mathf.Lerp (m_velocityY, 0, Time.deltaTime * m_smoothTime);
		}
	}

	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f) {
			angle += 360f;
		}
		if (angle > 360f) {
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}
}