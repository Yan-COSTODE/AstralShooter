using System.Collections.Generic;
using UnityEngine;

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
	[SerializeField] protected EEnemy type;
	[Header("Stat")]
	[SerializeField] protected Stat health;
	[SerializeField] protected Stat shield;
	[SerializeField] protected Stat damage;
	[SerializeField] protected MovementPattern movement;
	[SerializeField] protected ulong iScore;
	[SerializeField] protected LootTable lootTable;
	[Header("Regen")] 
	[SerializeField] private bool bCanRegenHealth;
	[SerializeField] private float fRegenHealth = 1.0f;
	[SerializeField] private float fRegenShield = 2.0f;
	[SerializeField] private float fRegenDelay = 3.0f;
	[Header("Weapon")]
	[SerializeField] protected Projectiles projectiles;
	[SerializeField] protected List<Transform> sockets;
	[Header("Visual")]
	[SerializeField] protected GameObject shieldVisual;
	private float fLastDamage = 0.0f;
	private Vector3 startPos;
	private float fAngle = 0.0f;
	private FloatingDamage floatingDamage = null;
	#endregion
	
	#region Properties
	public Vector3 StartPos => startPos;
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
	    damage.Init();
	    startPos = transform.position;
    }

    private void Update()
    {
	    Regen();
	    
	    if (movement)
		    movement.SetNextPosition(this);
    }

    private void OnCollisionEnter2D(Collision2D _other)
    {
	    GameObject _gO = _other.gameObject;
		
	    if (_gO.GetComponent<Projectiles>())
		    HitProjectile(_gO.GetComponent<Projectiles>());
	    if (_gO.GetComponent<Player>())
		    HitPlayer(_gO.GetComponent<Player>());
    }
    
    private void Regen()
    {
	    fLastDamage += Time.deltaTime;
	    
	    if (fLastDamage < fRegenDelay)
		    return;
		
	    shield.AddCurrent(fRegenShield * Time.deltaTime);
	    shieldVisual.SetActive(shield.Current >= 0.0f);
		
	    if (bCanRegenHealth)
		    health.AddCurrent(fRegenHealth * Time.deltaTime);
    }
    
    private void HitProjectile(Projectiles _projectiles)
    {
	    TakeDamage(_projectiles.Damage.Current);
	    _projectiles.Touch();
    }

    private void HitPlayer(Player _player)
    {
	    _player.TakeDamage(health.Current + shield.Current);
	    Die(false);
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
		    shieldVisual.SetActive(shield.Current >= 0.0f);
	    }
	    else
		    health.RemoveCurrent(_damage);

	    if (health.Current <= 0)
		    Die();
    }

    private void Die(bool _score = true)
    {
	    if (_score)
	    {
		    lootTable.Roll(transform, Player.Instance.Luck.Current);
		    Player.Instance.AddScore(iScore);
	    }
		
	    Destroy(gameObject);
    }
    #endregion
}
