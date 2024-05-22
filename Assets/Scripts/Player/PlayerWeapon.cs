using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Weapon", menuName = "Custom/Player Weapon")]
public class PlayerWeapon : ScriptableObject
{
	#region Fields & Properties
	#region Fields
    [SerializeField] private Stat damage;
    [SerializeField] private Stat reload;
    [SerializeField] private GameObject visual;
    [SerializeField] private GameObject projectile;
    private int socketIndex;
    private List<Transform> sockets;
    private GameObject instance;
	#endregion
	
	#region Properties
	public Stat Damage => damage;
	#endregion
	#endregion
	
	#region Methods
	public void Reload()
	{
		reload.AddCurrent(Time.deltaTime);
	}
	
    public void Setup(Transform _parent)
    {
        instance = Instantiate(visual, _parent);
        sockets = instance.GetComponent<PlayerWeaponComp>().Sockets;
    }

    public void Shoot()
    {
	    if (reload.Current != reload.Max)
		    return;

	    reload.ChangeCurrent(0.0f);
	    socketIndex++;
	    socketIndex %= sockets.Count;
	    GameObject _projectile = Instantiate(projectile, sockets[socketIndex].position, Quaternion.identity);
	    _projectile.GetComponent<Projectiles>().Setup(this);
	    
    }
    #endregion
}
