using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[Header("Stat")] 
	[SerializeField] private Stat health;
	[SerializeField] private Stat shield;
	[SerializeField] private Stat luck;
	[SerializeField] private float fSuperShieldDuration = 5.0f;
	[Header("Regen")] 
	[SerializeField] private bool bCanRegenHealth;
	[SerializeField] private float fRegenHealth = 1.0f;
	[SerializeField] private float fRegenShield = 10.0f;
	[SerializeField] private float fRegenDelay = 3.0f;
	private float fLastDamage;
	private int iSuperShieldCharge;
	private float fSuperShieldTimer;
	private bool bSuperShield;
	private Player player;
	#endregion
	
	#region Properties
	public Stat Health => health;
	public Stat Shield => shield;
	public Stat Luck => luck;
	#endregion
	#endregion
	
	#region Methods
    private void Start()
    {
	    health.Init();
	    shield.Init();
	    luck.Init();
    }

    private void Update()
    {
	    Regen();
	    SuperShieldTimer();
    }
    
    public void Register(Player _player) => player = _player;

    public void TakeDamage(float _damage)
    {
	    fLastDamage = 0;
	    if (Shield.Current > 0)
	    {
		    UIManager.Instance.SpawnFloatingDamage(_damage, Color.cyan, transform.position);
		    Shield.RemoveCurrent(_damage);
	    }
	    else
	    {
		    UIManager.Instance.SpawnFloatingDamage(_damage, Color.green, transform.position);
		    Health.RemoveCurrent(_damage);
	    }

	    if (Health.Current <= 0)
		    player.Die();
    }

    private void Regen()
    {
	    fLastDamage += Time.deltaTime;
	    
	    if (bSuperShield || fLastDamage < fRegenDelay)
		    return;
		
	    Shield.AddCurrent(fRegenShield * Time.deltaTime);
		
	    if (bCanRegenHealth)
		    Health.AddCurrent(fRegenHealth * Time.deltaTime);
    }

    private void SuperShieldTimer()
    {
	    if (!bSuperShield)
		    return;

	    fSuperShieldTimer += Time.deltaTime;

	    if (fSuperShieldTimer < fSuperShieldDuration)
		    return;
	    
	    bSuperShield = false;
	    fSuperShieldTimer = 0.0f;
    }

    public void AddSuperShield()
    {
	    iSuperShieldCharge++;
    }
    
    public void ActivateSuperShield()
    {
	    if (iSuperShieldCharge <= 0 || bSuperShield)
		    return;

	    iSuperShieldCharge--;
	    bSuperShield = true;
    }
    #endregion
}
