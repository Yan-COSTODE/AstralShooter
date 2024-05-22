using UnityEngine;

public class Player : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[Header("Component")]
	[SerializeField] private PlayerMovement movement;
	[SerializeField] private Transform weaponSocket;
	[SerializeField] private Transform engineSocket;
    [Header("Stats")]
	[SerializeField] private PlayerWeapon weapon;
    [SerializeField] private Stat health;
    [SerializeField] private Stat shield;
    [Header("Regen")] 
    [SerializeField] private bool bCanRegenHealth;
    [SerializeField] private float regenHealth = 5.0f;
    [SerializeField] private float regenShield = 1.0f;
    [SerializeField] private float regenDelay = 5.0f;
    private float lastDamage;
	#endregion
	
	#region Properties
	public Stat Health => health;
	public Stat Shield => shield;
	#endregion
	#endregion
	
	#region Methods

	private void Start()
	{
		health.Init();
		shield.Init();
		weapon.Setup(weaponSocket);
	}

	private void Update()
	{
		Regen();
		weapon.Reload();
		
		if (Input.GetKeyDown(KeyCode.E))
			TakeDamage(20);
		if (Input.GetKey(KeyCode.Space))
			weapon.Shoot();
	}

	public void TakeDamage(float _damage)
	{
		lastDamage = 0;
		if (shield.Current > 0)
			shield.RemoveCurrent(_damage);
		else
			health.RemoveCurrent(_damage);
	}

	public void Regen()
	{
		lastDamage += Time.deltaTime;
		if (lastDamage < regenDelay)
			return;
		
		shield.AddCurrent(regenShield * Time.deltaTime);
		
		if (bCanRegenHealth)
			health.AddCurrent(regenHealth * Time.deltaTime);
	}
	#endregion
}
