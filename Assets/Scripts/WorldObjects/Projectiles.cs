using UnityEngine;

public class Projectiles : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private float fFlySpeed;
	private Stat damage;
	#endregion
	
	#region Properties
	public Stat Damage => damage;
	#endregion
	#endregion
	
	#region Methods
	private void Update()
	{
		transform.position += transform.up * (fFlySpeed * Time.deltaTime);
	}

	private void OnCollisionEnter2D(Collision2D _other)
	{
		GameObject _gO = _other.gameObject;

		if (_gO.GetComponent<Projectiles>())
		{
			_gO.GetComponent<Projectiles>().Touch();
			Touch();
		}
		if (_gO.GetComponent<Player>())
		{
			_gO.GetComponent<Player>().TakeDamage(damage.Current);
			Touch();
		}
	}
	
	public void Setup(Weapon weapon)
	{
		damage = weapon.Damage;
	}

	public void Touch()
	{
		Destroy(gameObject);
	}
	#endregion
}
