using System;
using UnityEngine;

public enum ELoot
{
	WEAPON,
	SHIELD,
	ENGINE,
	SUPER_SHIELD
}

public class Loot : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private ELoot category;
	[SerializeField] private Weapon weapon;
	[SerializeField] private float fFlySpeed = 0.5f;
	#endregion
	
	#region Properties
	public ELoot Category => category;
	public Weapon Weapon => weapon;
	#endregion
	#endregion
	
	#region Methods
	private void Update()
	{
		transform.position += Vector3.down * (fFlySpeed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.GetComponent<Player>())
			PlayerPickup(_other.GetComponent<Player>());
	}

	private void PlayerPickup(Player _player)
	{
		_player.PickUpLoot(this);
		SoundManager.Instance.Play(ESound.LOOT, transform.position, 2.0f, false, false);
		Destroy(gameObject);
	}
	#endregion
}
