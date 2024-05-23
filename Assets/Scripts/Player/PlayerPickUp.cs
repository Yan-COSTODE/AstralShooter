using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private int iMaxWeaponLevel = 5;
	[SerializeField] private int iMaxShieldLevel = 5;
	[SerializeField] private int iMaxEngineLevel = 5;
	private Player player;
	private int weaponLevel;
	private int shieldLevel;
	private int engineLevel;
	#endregion
	
	#region Properties
	public int WeaponLevel;
	public int ShieldLevel;
	public int EngineLevel;
	#endregion
	#endregion
	
	#region Methods
	public void Register(Player _player) => player = _player;
	
	public void PickUp(Loot _loot)
	{
		switch (_loot.Category)
		{
			case ELoot.WEAPON: PickUpWeapon(_loot.Weapon);
				break;
			case ELoot.SHIELD: PickUpShield();
				break;
			case ELoot.ENGINE: ;
				break;
			case ELoot.SUPER_SHIELD: PickUpSuperShield();
				break;
			default: ;
				break;
		}
	}
	
	private void PickUpWeapon(PlayerWeapon _weapon)
	{
		if (!_weapon)
			return;
		
		if (!player.Armory.Weapon || player.Armory.Weapon.Id != _weapon.Id)
		{
			player.Armory.ChangeWeapon(_weapon);
			weaponLevel = 0;
		}
		else if (player.Armory.Weapon.Id == _weapon.Id)
		{
			if (weaponLevel < iMaxWeaponLevel)
				weaponLevel++;
		}
	}

	private void PickUpShield()
	{
		if (shieldLevel < iMaxShieldLevel)
			shieldLevel++;
		
		player.Shield.AddMult(0.5f);
	}

	private void PickUpSuperShield() => player.Stats.AddSuperShield();
	#endregion
}
