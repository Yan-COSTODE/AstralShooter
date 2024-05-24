using UnityEngine;

public class Asteroids : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private LootTable lootTable;
	[SerializeField] private FloatRange moveSpeed;
	[SerializeField] private FloatRange rotateSpeed;
	[SerializeField] private float fHealth = 10.0f;
	[SerializeField] private ulong iScore = 100;
	private float fFlySpeed = 0.0f;
	private float fRotSpeed = 0.0f;
	private FloatingDamage floatingDamage= null;
	#endregion

	#region Properties
	#endregion
	#endregion

	#region Methods
	private void Start()
	{
		fFlySpeed = moveSpeed.Value;
		fRotSpeed = rotateSpeed.Value;
		lootTable.Init();
	}

	private void Update()
	{
		transform.position += Vector3.down * (fFlySpeed * Time.deltaTime);
		transform.eulerAngles += Vector3.forward * (fRotSpeed * Time.deltaTime);
	}

	private void OnCollisionEnter2D(Collision2D _other)
	{
		GameObject _gO = _other.gameObject;
		
		if (_gO.GetComponent<Projectiles>())
			HitProjectile( _gO.GetComponent<Projectiles>());
		if (_gO.GetComponent<Player>())
			HitPlayer( _gO.GetComponent<Player>());
	}

	private void HitProjectile(Projectiles _projectiles)
	{
		TakeDamage(_projectiles.Damage.Current);
		_projectiles.Touch();
	}

	private void HitPlayer(Player _player)
	{
		_player.TakeDamage(fHealth);
		Die(false);
	}
	
	private void TakeDamage(float _damage)
	{
		if (!floatingDamage)
			floatingDamage = UIManager.Instance.SpawnFloatingDamage(_damage, Color.grey, transform.position);
		else
			floatingDamage.Set(_damage, Color.grey, transform.position);
		fHealth -= _damage;
		if (fHealth <= 0.0f)
			Die();
	}
	
	private void Die(bool _score = true)
	{
		if (_score)
		{
			lootTable.Roll(transform, Player.Instance.Luck.Current);
			Player.Instance.AddScore(iScore);
		}
		
		Destroy(gameObject);
	}
	#endregion
}
