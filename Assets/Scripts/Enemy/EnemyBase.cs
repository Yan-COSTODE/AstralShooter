using UnityEngine;
using UnityEngine.UI;

public enum EEnemy
{
	DEFAULT,
	BATTLECRUISER,
	BOMBER,
	DREADNOUGHT,
	FIGHTER,
	FRIGATE,
	SCOUT,
	SUPPORT,
	TORPEDO
}

public class EnemyBase : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private EEnemy type;
	[SerializeField] private EnemySpawner spawner;
	[SerializeField] private EnemyObjectSpawner objectSpawner;
	[Header("Stat")]
	[SerializeField] private Stat health;
	[SerializeField] private Stat shield;
	[SerializeField] private EScriptable movementPattern = EScriptable.MOVEMENT_WAVE;
	[SerializeField] private ulong iScore;
	[SerializeField] private LootTable lootTable;
	[Header("Go To")] 
	[SerializeField] private bool bGoTo;
	[SerializeField] private float fGoToSpeed = 1.0f;
	[SerializeField] private Vector3 goToPosition;
	[Header("Regen")] 
	[SerializeField] private bool bCanRegenHealth;
	[SerializeField] private float fRegenHealth = 1.0f;
	[SerializeField] private float fRegenShield = 2.0f;
	[SerializeField] private float fRegenDelay = 3.0f;
	[Header("Weapon")] 
	[SerializeField] private EScriptable weaponPattern = EScriptable.KLAED_FIGHTER;
	[SerializeField] private Transform weaponSocket;
	[Header("Visual")] 
	[SerializeField] private Image shieldBar;
	[SerializeField] private Image healthBar;
	[SerializeField] private Animator animator;
	[SerializeField] private float fDieDelay = 0.5f;
	[SerializeField] private GameObject shieldVisual;
	[SerializeField] private GameObject engineVisual;
	private float fLastDamage = 0.0f;
	private Vector3 startPos;
	private float fAngle = 0.0f;
	private FloatingDamage floatingDamage = null;
	private bool bDead = false;
	private MovementPattern movement;
	private Weapon weapon;
	#endregion
	
	#region Properties
	public Vector3 StartPos => startPos;
	public Weapon Weapon => weapon;
	public float Angle
	{
		get => fAngle;
		set => fAngle = value;
	}
	#endregion
	#endregion
	
	#region Methods
    private void Start()
    {
	    health.Init();
	    shield.Init();
	    startPos = transform.position;
	    movement = ScriptableManager.Instance.Get<MovementPattern>(movementPattern);
	    weapon = ScriptableManager.Instance.Get<Weapon>(weaponPattern);
	    
	    if (weapon)
		    weapon.Setup(weaponSocket);
    }

    private void Update()
    {
	    if (bDead)
		    return;
	    
	    Regen();
	    Move();
		UpdateBar();
	    
	    if (!bGoTo && weapon)
	    {
		    StartCoroutine(weapon.Shoot(0));
		    weapon.Reload();
	    }
    }

    private void Move()
    {
	    if (bGoTo)
	    {
		    Vector3 _dir = goToPosition - transform.position;
		    Vector3 _normalized = _dir.normalized;
		    Vector3 _delta = _normalized * (fGoToSpeed * Time.deltaTime);

		    if (_delta.magnitude >= _dir.magnitude)
		    {
			    transform.position = goToPosition;
			    startPos = transform.position;
			    bGoTo = false;
		    }
		    else
			    transform.position += _delta;
	    }
	    else
	    {
		    if (movement)
			    movement.SetNextPosition(this);
	    }
    }
    
    private void OnCollisionEnter2D(Collision2D _other)
    {
	    GameObject _gO = _other.gameObject;
		
	    if (_gO.GetComponent<Projectiles>())
		    HitProjectile(_gO.GetComponent<Projectiles>());
	    if (_gO.GetComponent<Player>())
		    HitPlayer(_gO.GetComponent<Player>());
    }

    private void UpdateBar()
    {
	    if (healthBar)
			healthBar.fillAmount = health.Percent;
	    
	    if (shieldBar)
			shieldBar.fillAmount = shield.Percent;
    }
    
    private void Regen()
    {
	    fLastDamage += Time.deltaTime;
	    
	    if (fLastDamage < fRegenDelay)
		    return;
		
	    shield.AddCurrent(fRegenShield * Time.deltaTime);
	    shieldVisual.SetActive(shield.Current > 0.0f);
		
	    if (bCanRegenHealth)
		    health.AddCurrent(fRegenHealth * Time.deltaTime);
    }
    
    private void HitProjectile(Projectiles _projectiles)
    {
	    TakeDamage(_projectiles.Damage.Current);
	    _projectiles.Touch(false);
    }

    private void HitPlayer(Player _player)
    {
	    _player.TakeDamage(health.Current + shield.Current);
    }
    
    private void TakeDamage(float _damage)
    {
	    fLastDamage = 0.0f;
	    
	    if (!floatingDamage)
		    floatingDamage = UIManager.Instance.SpawnFloatingDamage(_damage, Color.red, transform.position);
	    else
		    floatingDamage.Set(_damage, Color.red, transform.position);
	    
	    if (shield.Current > 0)
	    {
		    shield.RemoveCurrent(_damage);
		    shieldVisual.SetActive(shield.Current > 0.0f);
	    }
	    else
		    health.RemoveCurrent(_damage);

	    if (health.Current <= 0)
		    Die();
    }

    public void Die(bool _score = true)
    {
	    if (_score)
	    {
		    lootTable.Roll(transform, Player.Instance.Luck.Current);
		    Player.Instance.AddScore(iScore);
	    }

	    if (spawner)
		    spawner.Destroy();
	    if (objectSpawner)
		    objectSpawner.Destroy();
	    
	    
	    GetComponent<Collider2D>().enabled = false;
	    engineVisual.SetActive(false);
	    weaponSocket.gameObject.SetActive(false);
	    shieldVisual.SetActive(false);
	    bDead = true;
	    animator.SetTrigger("Die");
	    SoundManager.Instance.Play(ESound.ENEMY_DIE, transform.position, 1.0f, false, false);
	    Destroy(gameObject, fDieDelay);
    }
    #endregion
}
