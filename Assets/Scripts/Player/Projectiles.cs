using System;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private Stat flySpeed;
	private Stat damage;
	#endregion
	
	#region Properties
	#endregion
	#endregion
	
	#region Methods
	private void Start()
	{
		flySpeed.Init();
	}
	
	private void Update()
	{
		transform.position += transform.up * (flySpeed.Current * Time.deltaTime);
	}

	public void Setup(PlayerWeapon _playerWeapon)
	{
		damage = _playerWeapon.Damage;
	}
	#endregion
}
