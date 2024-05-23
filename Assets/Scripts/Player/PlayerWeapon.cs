using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Weapon", menuName = "Custom/Player Weapon")]
public class PlayerWeapon : ScriptableObject
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private string id;
    [SerializeField] private Stat damage;
    [SerializeField] private Stat reload;
    [SerializeField] private Stat salveDelay;
    [SerializeField] private Stat salveInitialDelay;
    [SerializeField] private PlayerWeaponComp visual;
    [SerializeField] private Projectiles projectile;
    private List<Transform> sockets;
    private float fAnimationSpeed = 1.0f;
    private int iLevel;
    private bool bSalve;
    private float fSalveTimer;
	#endregion
	
	#region Properties
	public Stat Damage => damage;
	public string Id => id;
	#endregion
	#endregion
	
	#region Methods
	public void Reload()
	{
		if (bSalve)
			return;
		
		reload.AddCurrent(Time.deltaTime);
	}
	
    public void Setup(Transform _parent)
    {
	    PlayerWeaponComp _instance = Instantiate(visual, _parent);
        sockets = _instance.Sockets;
        reload.Init();
        damage.Init();
        salveDelay.Init();
        salveInitialDelay.Init();
    }

    public IEnumerator Shoot(int _level)
    {
	    ChangeLevel(_level);
	    if (reload.Current != reload.Max)
		    yield break;

	    reload.ChangeCurrent(0.0f);
	    yield return ShootSalve();
    }

    private IEnumerator ShootSalve()
    {
	    yield return new WaitForSeconds(salveInitialDelay.Current);
	    
	    for (int _i = 0; _i < sockets.Count; _i++)
	    {
		    Projectiles _projectile = Instantiate(projectile, sockets[_i].position, Quaternion.identity);
		    _projectile.Setup(this);
		    yield return new WaitForSeconds(salveDelay.Current);
	    }
    }
    
    private void ChangeLevel(int _level)
    {
	    if (iLevel == _level)
		    return;

	    int _diff = _level - iLevel;
	    if (_diff > 0)
	    {
		    for (int _i = 0; _i < _diff; _i++)
		    {
			    damage.AddMult(0.5f);
			    reload.AddMult(-0.1f);
			    salveDelay.AddMult(-0.1f);
			    salveInitialDelay.AddMult(-0.1f);
		    }
	    }
	    else
	    {
		    for (int _i = 0; _i < -_diff; _i++)
		    {
			    damage.RemoveMult(0.5f);
			    reload.RemoveMult(-0.1f);
			    salveDelay.RemoveMult(-0.1f);
			    salveInitialDelay.RemoveMult(-0.1f);
		    }
	    }

	    fAnimationSpeed = 1 * salveDelay.CalculateMult();
    }
    #endregion
}
