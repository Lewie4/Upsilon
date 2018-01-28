using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
	[Tooltip ("UI Text to display Player's Name")]
	public Text PlayerNameText;
	[Tooltip("Pixel offset from the player target")]
	public Vector3 ScreenOffset = new Vector3(0f,30f,0f);

	NetworkPlayerManager _target;
	float _characterControllerHeight = 0f;
	Transform _targetTransform;
	Vector3 _targetPosition;

	void Awake()
	{
		this.GetComponent<Transform>().SetParent (GameObject.Find("Canvas").GetComponent<Transform>());
	}

	public void SetTarget(NetworkPlayerManager target)
	{
		if (target == null) 
		{
			Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.",this);
			return;
		}
		// Cache references for efficiency
		_target = target;

		CharacterController _characterController = _target.GetComponent<CharacterController> ();
		// Get data from the Player that won't change during the lifetime of this Component
		if (_characterController != null)
		{
			_characterControllerHeight = _characterController.height;
		}

		if (PlayerNameText != null) 
		{
			PlayerNameText.text = _target.photonView.owner.name;
		}
	}

	private void Update()
	{
		// Destroy itself if the target is null, It's a fail safe when Photon is destroying Instances of a Player over the network
		if (_target == null) 
		{
			Destroy(this.gameObject);
			return;
		}
	}

	void LateUpdate() 
	{
		// #Critical
		// Follow the Target GameObject on screen.
		if (_targetTransform!=null)
		{
			_targetPosition = _targetTransform.position;
			_targetPosition.y += _characterControllerHeight;
			this.transform.position = Camera.main.WorldToScreenPoint (_targetPosition) + ScreenOffset;
		}
	}
}
