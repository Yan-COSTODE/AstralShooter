using UnityEngine;

[CreateAssetMenu(fileName = "Circular Pattern", menuName = "Custom/Pattern/Circular Pattern")]
public class CircularMovementPattern : MovementPattern
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private float fRadius;
	#endregion
	
	#region Properties
	#endregion
	#endregion
	
	#region Methods
	public override void SetNextPosition(EnemyBase _base)
	{
		_base.Angle += fSpeed * Time.deltaTime;
		float _x = Mathf.Cos(_base.Angle) * fRadius;
		float _y = Mathf.Sin(_base.Angle) * fRadius;
		_base.transform.position = _base.StartPos + new Vector3(_x, _y, 0);
	}

	#endregion
}
