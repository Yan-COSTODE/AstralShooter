using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private Camera reference;
	[Header("Stats")]
	[SerializeField] private Stat moveSpeed;
	[SerializeField] private float fMaxTilt = 30.0f;
	[SerializeField] private float fMultTilt = 2.0f;
	[SerializeField] private Vector2 screenOffset;
	private float _aspect = 0.0f;
	private float _ortho = 0.0f;
	private Vector2 _min;
	private Vector2 _max;
	#endregion
	
	#region Properties
	#endregion
	#endregion
	
	#region Methods

	private void Start()
	{
		if (reference)
		{
			_aspect = reference.aspect;
			_ortho = reference.orthographicSize;
		}

		_min.y = -_ortho;
		_min.x = _min.y * _aspect;
		_max.y = _ortho;
		_max.x = _max.y * _aspect;
	}
	
	private void Update()
	{
		MoveLR(Input.GetAxis("Horizontal"));
		MoveUD(Input.GetAxis("Vertical"));
		ClampMovement();
	}

	private void ClampMovement()
	{
		Vector2 _clamped;
		_clamped.x = Mathf.Clamp(transform.position.x, _min.x + screenOffset.x, _max.x - screenOffset.x);
		_clamped.y = Mathf.Clamp(transform.position.y, _min.y + screenOffset.y, _max.y - screenOffset.y);
		transform.position = new Vector3(_clamped.x, _clamped.y, transform.position.z);
	}

	private void MoveLR(float _axis)
	{
		float _fTargetAngle = _axis * fMaxTilt;
		float _fcurrentAngle = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.y, _fTargetAngle, moveSpeed.Value * fMultTilt * Time.deltaTime);
		transform.rotation = Quaternion.Euler(0.0f, _fcurrentAngle, 0.0f);
		transform.position += Vector3.right * (moveSpeed.Value * _axis * Time.deltaTime);
	}

	private void MoveUD(float _axis)
	{
		transform.position += Vector3.up * (moveSpeed.Value * _axis * Time.deltaTime);
	}
    #endregion
}
