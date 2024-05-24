using UnityEngine;

[CreateAssetMenu(fileName = "Wave Pattern", menuName = "Custom/Pattern/Wave Pattern")]
public class WaveMovementPattern : MovementPattern
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private float fHFrequency = 2.0f;
	[SerializeField] private float fHAmplitude = 1.0f;
	[SerializeField] private float fVFrequency = 2.0f;
	[SerializeField] private float fVAmplitude = 1.0f;
	#endregion
	
	#region Properties
	#endregion
	#endregion
	
	#region Methods
	public override void SetNextPosition(EnemyBase _base)
	{
		float _waveR = Mathf.Sin(Time.time * fHFrequency) * fHAmplitude;
		Vector3 _right = Vector3.right * (_waveR * fSpeed);
		float _waveU = Mathf.Sin(Time.time * fVFrequency) * fVAmplitude;
		Vector3 _up = Vector3.up * (_waveU * fSpeed);
		_base.transform.position = _base.StartPos + _right + _up;
	}
	#endregion
}
