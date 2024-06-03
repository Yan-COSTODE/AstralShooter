using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private int iMaxWeaponLevel = 5;
	[SerializeField] private int iMaxShieldLevel = 5;
	[SerializeField] private int iMaxEngineLevel = 5;
	private Player player = null;
	private int weaponLevel = 0;
	private int shieldLevel = 0;
	private int engineLevel = 0;
	#endregion
	
	#region Properties
	public int WeaponLevel => weaponLevel;
	public int ShieldLevel => shieldLevel;
	public int EngineLevel => engineLevel;
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
	
	private void PickUpWeapon(Weapon _weapon)
	{
		if (!_weapon)
			return;
		
		if (!player.Armory.Weapon || player.Armory.Weapon.Type != _weapon.Type)
		{
			weaponLevel = 0;
			player.Armory.ChangeWeapon(_weapon);
		}
		else if (player.Armory.Weapon.Type == _weapon.Type)
		{
			if (weaponLevel < iMaxWeaponLevel)
				weaponLevel++;
		}
	}

	private void PickUpShield()
	{
		if (shieldLevel < iMaxShieldLevel)
			shieldLevel++;
		
		player.Shield.AddMult(0.2f);
	}

	private void PickUpEngine()
	{
		if (engineLevel < iMaxEngineLevel)
			engineLevel++;
		
		player.MoveSpeed.AddMult(0.2f);
	}
	
	private void PickUpSuperShield() => player.Stats.AddSuperShield();
	#endregion
}
