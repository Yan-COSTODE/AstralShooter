using UnityEngine;

public class UIManager : SingletonTemplate<UIManager>
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private Player player;
	[SerializeField] private HUD hud;
	#endregion
	
	#region Properties
	public HUD HUD => hud;
	public Player Player => player;
	#endregion
	#endregion

	#region Methods

	private void Start()
	{
		if (!hud)
			hud = GetComponentInChildren<HUD>();
	}
	#endregion
}
