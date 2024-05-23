using UnityEngine;

public class PlayerArmory : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[Header("Sockets")]
	[SerializeField] private Transform weaponSocket;
	[SerializeField] private Transform engineSocket;
	[Header("Objects")]
	[SerializeField] private PlayerWeapon weapon;
	private Player player = null;
	#endregion
	
	#region Properties
	public PlayerWeapon Weapon => weapon;
	#endregion
	#endregion
	
	#region Methods
    private void Start()
    {
	    if (weapon)
		    weapon.Setup(weaponSocket);
    }

    private void Update()
    {
	    if (!weapon)
		    return;
	    
	    weapon.Reload();
    }

    public void Shoot()
    {
	    if (!weapon)
		    return;
	    
	    StartCoroutine(weapon.Shoot(player.PickUp.WeaponLevel));
    } 
    
    public void Register(Player _player) => player = _player;

    public void ChangeWeapon(PlayerWeapon _weapon)
    {
	    for (int _i = 0; _i < weaponSocket.childCount; _i++)
		    Destroy(weaponSocket.GetChild(_i).gameObject);

	    weapon = _weapon;
	    weapon.Setup(weaponSocket);
    }
    #endregion
}
