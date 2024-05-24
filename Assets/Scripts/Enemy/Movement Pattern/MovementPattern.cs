using UnityEngine;

public abstract class MovementPattern : ScriptableObject
{
	#region Fields & Properties
	#region Fields
	[SerializeField] protected float fSpeed = 5.0f;
	#endregion
	
	#region Properties
	#endregion
	#endregion
	
	#region Methods
	public abstract void SetNextPosition(EnemyBase _base);
	#endregion
}
