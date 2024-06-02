using UnityEngine;
using UnityEngine.SceneManagement;

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
	[SerializeField] private PlayerInputs inputs;
	private bool bDead = false;
	private ulong iScore = 0;
	#endregion
	
	#region Properties
	public Stat Health => stats.Health;
	public Stat Shield => stats.Shield;
	public Stat Luck => stats.Luck;
	public Stat MoveSpeed => movement.MoveSpeed;
	public PlayerArmory Armory => armory;
	public PlayerStats Stats => stats;
	public PlayerPickUp PickUp => pickUp;
	public PlayerMovement Movement => movement;
	public PlayerInputs Inputs => inputs;
	public bool Dead => bDead;
	public ulong Score => iScore;
	public string Username => username;
	#endregion
	#endregion
	
	#region Methods
	private void Start()
	{
		SceneManager.activeSceneChanged += SceneChangement;
		SceneChangement(default, default);
		username = PlayerPrefs.GetString("PlayerName", "Player");
		stats.Register(this);
		pickUp.Register(this);
		armory.Register(this);
		movement.Register(this);
		inputs.Register(this);
	}

	private void OnDestroy()
	{
		SceneManager.activeSceneChanged -= SceneChangement;
	}

	private void SceneChangement(Scene _scene1, Scene _scene2)
	{
		movement.UpdateReference();
		UIManager.Instance.HUD.Init();
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
