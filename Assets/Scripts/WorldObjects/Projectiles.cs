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
