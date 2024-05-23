using UnityEngine;

public class UIManager : SingletonTemplate<UIManager>
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private HUD hud;
	[SerializeField] private FloatingDamage floatingDamage;
	#endregion
	
	#region Properties
	public HUD HUD => hud;
	#endregion
	#endregion

	#region Methods
	private void Start()
	{
		if (!hud)
			hud = GetComponentInChildren<HUD>();
	}

	public FloatingDamage SpawnFloatingDamage(float _damage,  Color _color, Vector3 _position)
	{
		FloatingDamage _floatingDamage = Instantiate(floatingDamage);
		_floatingDamage.Set(_damage, _color, _position);
		return _floatingDamage;
	}
	#endregion
}
