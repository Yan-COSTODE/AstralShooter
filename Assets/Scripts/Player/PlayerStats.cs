using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[Header("Visual")]
	[SerializeField] private GameObject shieldVisual;
	[SerializeField] private GameObject superShieldVisual;
	[SerializeField] private SpriteRenderer visualRenderer;
	[SerializeField] private LifeSprite lifeSprite;
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
	private float fLastDamage = 0.0f;
	private int iSuperShieldCharge = 0;
	private float fSuperShieldTimer = 0.0f;
	private bool bSuperShield = false;
	private Player player = null;
	private FloatingDamage floatingDamage = null;
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
	    shieldVisual.SetActive(false);
	    superShieldVisual.SetActive(false);
	    lifeSprite.Init();
	    health.Init();
	    shield.Init();
	    luck.Init();
    }

    private void Update()
    {
	    Regen();
	    SuperShieldTimer();
	    shieldVisual.SetActive(shield.Current > 0);
    }
    
    public void Register(Player _player) => player = _player;

    public void TakeDamage(float _damage)
    {
	    fLastDamage = 0;

	    if (bSuperShield)
		    return;
	    
	    if (Shield.Current > 0)
	    {
		    ShowDamage(_damage, Color.cyan);
		    shield.RemoveCurrent(_damage);
	    }
	    else
	    {
		    ShowDamage(_damage, Color.green);
		    health.RemoveCurrent(_damage);
	    }
	    
	    Sprite _sprite = lifeSprite.GetSprite(health.Percent);
	    
	    if (_sprite)
		    visualRenderer.sprite = _sprite;
	    
	    if (Health.Current <= 0)
		    player.Die();
    }

    private void ShowDamage(float _damage, Color _color)
    {
	    if (!floatingDamage)
		    floatingDamage = UIManager.Instance.SpawnFloatingDamage(_damage, _color, transform.position);
	    else
		    floatingDamage.Set(_damage, _color, transform.position);
    }
    
    private void Regen()
    {
	    fLastDamage += Time.deltaTime;
	    
	    if (bSuperShield || fLastDamage < fRegenDelay)
		    return;
		
	    shield.AddCurrent(fRegenShield * Time.deltaTime);

	    if (!bCanRegenHealth)
		    return;
	    
		health.AddCurrent(fRegenHealth * Time.deltaTime);
		Sprite _sprite = lifeSprite.GetSprite(health.Percent);
		
		if (_sprite)
			visualRenderer.sprite = _sprite;
    }

    private void SuperShieldTimer()
    {
	    if (!bSuperShield)
		    return;

	    fSuperShieldTimer += Time.deltaTime;

	    if (fSuperShieldTimer < fSuperShieldDuration)
		    return;
	    
	    superShieldVisual.SetActive(false);
	    bSuperShield = false;
	    fSuperShieldTimer = 0.0f;
    }

    public void AddSuperShield()
    {
	    if (iSuperShieldCharge >= 7)
		    return;
	    
	    iSuperShieldCharge++;
	    UIManager.Instance.HUD.SetNumberofCharge(iSuperShieldCharge);
    }
    
    public void ActivateSuperShield()
    {
	    if (iSuperShieldCharge <= 0 || bSuperShield)
		    return;

	    superShieldVisual.SetActive(true);
	    iSuperShieldCharge--;
	    UIManager.Instance.HUD.SetNumberofCharge(iSuperShieldCharge);
	    bSuperShield = true;
    }
    #endregion
}
