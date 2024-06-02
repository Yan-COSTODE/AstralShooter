using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private Animator animator;
	[SerializeField] private Transform rotationTransform;
	[SerializeField] private Camera reference;
	[Header("Stats")]
	[SerializeField] private Stat moveSpeed;
	[SerializeField] private float fMaxTilt = 30.0f;
	[SerializeField] private float fMultTilt = 2.0f;
	[SerializeField] private Vector2 screenOffset;
	private Vector2 min = Vector2.zero;
	private Vector2 max = Vector2.zero;
	private Player player = null;
	private float fMove = 0.0f;
	#endregion
	
	#region Properties
	public Stat MoveSpeed => moveSpeed;
	public bool Moving => fMove != 0.0f;
	#endregion
	#endregion
	
	#region Methods
	private void Start()
	{
		Init();
		moveSpeed.Init();
	}
	
	private void Update()
	{
		if (player.Dead)
			return;

		ClampMovement();
		
		if (animator)
			animator.SetBool("bMoving", Moving);
	}

	public void Register(Player _player) => player = _player;

	public void UpdateReference()
	{
		if (reference)
			return;
		
		reference = Camera.current;

		if (reference)
			return;

		reference = Camera.main;
		Init();
	}
	
	private void Init()
	{
		float _ortho = 0;
		float _aspect = 0;
		
		if (reference)
		{
			_aspect = reference.aspect;
			_ortho = reference.orthographicSize;
		}

		min.y = - _ortho;
		min.x = min.y * _aspect;
		max.y = _ortho;
		max.x = max.y * _aspect;
	}

	public void ResetMove() => fMove = 0.0f;
	
	private void ClampMovement()
	{
		Vector2 _clamped;
		_clamped.x = Mathf.Clamp(transform.position.x, min.x + screenOffset.x, max.x - screenOffset.x);
		_clamped.y = Mathf.Clamp(transform.position.y, min.y + screenOffset.y, max.y - screenOffset.y);
		transform.position = new Vector3(_clamped.x, _clamped.y, transform.position.z);
	}

	private void Rotate(float _axis)
	{
		float _fTargetAngle = _axis * fMaxTilt;
		float _fcurrentAngle = Mathf.MoveTowardsAngle(rotationTransform.rotation.eulerAngles.y, _fTargetAngle, moveSpeed.Current * fMultTilt * Time.deltaTime);
		rotationTransform.rotation = Quaternion.Euler(0.0f, _fcurrentAngle, 0.0f);
	}

	public void SetPosition(Vector3 _position)
	{
		transform.position = _position;
	}
	
	public void MoveHorizontal(float _axis)
	{
		Rotate(_axis);
		transform.position += Vector3.right * (moveSpeed.Current * _axis * Time.deltaTime);
		fMove += Mathf.Abs(_axis);
	}
	
	public void MoveVertical(float _axis)
	{
		transform.position += Vector3.up * (moveSpeed.Current * _axis * Time.deltaTime);
		fMove += Mathf.Abs(_axis);
	}
    #endregion
}
