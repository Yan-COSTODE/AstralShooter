using System;
using UnityEngine;

public enum EProjectileType
{
	NORMAL,
	HOMING,
	RAY,
	BOMB
}

public class Projectiles : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private float fFlySpeed;
	[SerializeField] private float fRotationSpeed;
	[SerializeField] private bool bDestructible = true;
	[SerializeField] private EProjectileType type = EProjectileType.NORMAL;
	[SerializeField] private float fRayLifetime = 2.0f;
	private Vector3 destination;
	private Stat damage;
	private bool bHoming = true;
	#endregion
	
	#region Properties
	public Stat Damage => damage;
	public EProjectileType Type => type;
	#endregion
	#endregion
	
	#region Methods
	private void Start()
	{
		if (type == EProjectileType.RAY)
			Destroy(gameObject, fRayLifetime);
		else if (type == EProjectileType.BOMB)
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
	}

	private void Update()
	{
		Move();
	}

	private void Move()
	{
		switch (type)
		{
			case EProjectileType.NORMAL: MoveNormal();
				break;
			case EProjectileType.BOMB: MoveNormal();
				break;
			case EProjectileType.HOMING: MoveHoming();
				break;
			case EProjectileType.RAY: ;
				break;
			default: ;
				break;
		}
	}
	
	private void MoveNormal()
	{
		transform.position += transform.up * (fFlySpeed * Time.deltaTime);
	}
	
	private void MoveHoming()
	{
		if (bHoming)
		{
			Vector3 _direction = (destination - transform.position).normalized;
			float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
			Quaternion _lookRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, _angle - 90.0f));
			transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, fRotationSpeed * Time.deltaTime);

			if (Quaternion.Angle(_lookRotation , transform.rotation) <= 10.0f || Vector3.Distance(destination, transform.position) <= 0.2f)
				bHoming = false;
		}

		transform.position += transform.up * (fFlySpeed * Time.deltaTime);
	}

	private void OnTriggerStay2D(Collider2D _other)
	{
		if (type != EProjectileType.RAY)
			return;
		
		GameObject _gO = _other.gameObject;
		
		if (_gO.GetComponent<Projectiles>())
			_gO.GetComponent<Projectiles>().Touch(true);
		
		if (_gO.GetComponent<Player>())
			_gO.GetComponent<Player>().TakeDamage(damage.Current * Time.deltaTime);
	}

	private void OnCollisionEnter2D(Collision2D _other)
	{
		if (type == EProjectileType.RAY)
			return;
		
		GameObject _gO = _other.gameObject;

		if (_gO.GetComponent<Projectiles>())
		{
			_gO.GetComponent<Projectiles>().Touch(true);
			Touch(true);
		}
		
		if (_gO.GetComponent<Player>())
		{
			_gO.GetComponent<Player>().TakeDamage(damage.Current);
			Touch(false);
		}
	}
	
	public void Setup(Weapon weapon)
	{
		damage = weapon.Damage;

		if (type == EProjectileType.HOMING)
		{
			if (Player.Instance)
				destination = Player.Instance.transform.position;
			else
				destination = new Vector3(0.0f, -3.0f, 0.0f);
		}
			
	}

	public void Touch(bool _projectile)
	{
		if (!bDestructible && _projectile)
			return;
		
		Destroy(gameObject);
	}
	#endregion
}
