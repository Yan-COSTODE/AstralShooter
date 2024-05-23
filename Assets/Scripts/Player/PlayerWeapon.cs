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
    private List<Transform> sockets = new();
    private float fAnimationSpeed = 1.0f;
    private int iLevel = 0;
    private bool bSalve = false;
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
        iLevel = 0;
        fAnimationSpeed = 1.0f;
        bSalve = false;
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

	    int _diff = iLevel - _level;
	    iLevel = _level;
	    if (_diff > 0)
	    {
		    for (int _i = 0; _i < _diff; _i++)
		    {
			    damage.AddMult(1.5f);
			    reload.AddMult(0.8f);
			    salveDelay.AddMult(0.8f);
			    salveInitialDelay.AddMult(0.8f);
		    }
	    }
	    else
	    {
		    for (int _i = 0; _i < -_diff; _i++)
		    {
			    damage.RemoveMult(1.5f);
			    reload.RemoveMult(0.8f);
			    salveDelay.RemoveMult(0.8f);
			    salveInitialDelay.RemoveMult(0.8f);
		    }
	    }
	    fAnimationSpeed = 1 * salveDelay.CalculateMult();
    }
    #endregion
}
