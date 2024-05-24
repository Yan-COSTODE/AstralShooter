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

	public void Setup(PlayerWeapon _playerWeapon)
	{
		damage = _playerWeapon.Damage;
	}

	public void Touch()
	{
		Destroy(gameObject);
	}
	#endregion
}
