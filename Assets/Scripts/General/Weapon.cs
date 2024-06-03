using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Custom/Weapon")]
public class Weapon : ScriptableObject
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private EScriptable type;
    [SerializeField] private Stat damage;
    [SerializeField] private Stat reload;
    [SerializeField] private Stat salveDelay;
    [SerializeField] private Stat salveInitialDelay;
    [SerializeField] private WeaponComp visual;
    [SerializeField] private Projectiles projectile;
    [SerializeField] private float fInitialDelay;
    private List<Transform> sockets = new();
    private Animator animator = null;
    private float fAnimationSpeed = 1.0f;
    private int iLevel = 0;
    private bool bSalve = false;
	#endregion
	
	#region Properties
	public Stat Damage => damage;
	public EScriptable Type => type;
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
	    WeaponComp _instance = Instantiate(visual, _parent);
        sockets = _instance.Sockets;
        animator = _instance.Animator;
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
	    bSalve = true;
	    
	    if (fInitialDelay != 0.0f)
			yield return new WaitForSeconds(Random.Range(0.0f, fInitialDelay));
	    
	    fInitialDelay = 0.0f;
	    
	    if (animator)
	    {
		    animator.speed = fAnimationSpeed;
		    animator.SetBool("bShoot", true);
	    }
	    
	    yield return new WaitForSeconds(salveInitialDelay.Current);
	    
	    for (int _i = 0; _i < sockets.Count; _i++)
	    {
		    if (sockets[_i])
		    {
			    Projectiles _projectile = Instantiate(projectile, sockets[_i].position, sockets[_i].rotation);
			    _projectile.Setup(this);
		    }
		    
		    yield return new WaitForSeconds(salveDelay.Current);
	    }

	    if (animator)
		    animator.SetBool("bShoot", false);

	    bSalve = false;
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
			    damage.AddMult(0.5f);
			    reload.AddMult(-0.2f);
			    salveDelay.AddMult(-0.2f);
			    salveInitialDelay.AddMult(-0.2f);
		    }
	    }
	    else
	    {
		    for (int _i = 0; _i < -_diff; _i++)
		    {
			    damage.RemoveMult(0.5f);
			    reload.RemoveMult(-0.2f);
			    salveDelay.RemoveMult(-0.2f);
			    salveInitialDelay.RemoveMult(-0.2f);
		    }
	    }
	    fAnimationSpeed = 1 * salveDelay.CalculateMult();
    }
    #endregion
}
