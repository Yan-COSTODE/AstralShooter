using UnityEngine;

public class Player : SingletonTemplate<Player>
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private string username = "Player";
	[Header("Component")]
	[SerializeField] private PlayerMovement movement;
	[SerializeField] private PlayerStats stats;
	[SerializeField] private PlayerPickUp pickUp;
	[SerializeField] private PlayerArmory armory;
	private bool bDead = false;
	private ulong iScore = 0;
	#endregion
	
	#region Properties
	public Stat Health => stats.Health;
	public Stat Shield => stats.Shield;
	public Stat Luck => stats.Luck;
	public PlayerArmory Armory => armory;
	public PlayerStats Stats => stats;
	public PlayerPickUp PickUp => pickUp;
	public bool Dead => bDead;
	public ulong Score => iScore;
	public string Username => username;
	#endregion
	#endregion
	
	#region Methods
	private void Start()
	{
		username = PlayerPrefs.GetString("PlayerName", "Player");
		stats.Register(this);
		pickUp.Register(this);
		armory.Register(this);
		movement.Register(this);
	}
	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
			TakeDamage(20);
		if (Input.GetKey(KeyCode.Space))
			armory.Shoot();
		if (Input.GetKeyDown(KeyCode.Q))
			stats.ActivateSuperShield();
		if (Input.GetKeyDown(KeyCode.Escape))
			UIManager.Instance.UIPauseMenu.Open();
		
	}
	
	public void AddScore(ulong _score)
	{
		iScore += _score;
	}

	public void TakeDamage(float _damage) => stats.TakeDamage(_damage);

	public void PickUpLoot(Loot _loot) => pickUp.PickUp(_loot);

	public void Die(bool _ui = true)
	{
		bDead = true;
		GetComponent<Collider2D>().enabled = false;
		Scoreboard.Instance.AddScore(iScore, username);
		
		if (_ui)
			UIManager.Instance.UIPauseMenu.Open(false);
	}
	#endregion
}
