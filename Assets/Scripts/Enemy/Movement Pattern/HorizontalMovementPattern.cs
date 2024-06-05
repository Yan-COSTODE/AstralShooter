using UnityEngine;

[CreateAssetMenu(fileName = "Horizontal Pattern", menuName = "Custom/Pattern/Horizontal Pattern")]
public class HorizontalMovementPattern : MovementPattern
{
    #region Fields & Properties
    #region Fields
    [SerializeField] private float fFrequency = 2.0f;
    [SerializeField] private float fAmplitude = 1.0f;
    #endregion

    #region Properties
    #endregion
    #endregion
    
    #region Methods
    public override void SetNextPosition(EnemyBase _base)
    {
        float _waveR = Mathf.Sin(Time.time * fFrequency) * fAmplitude;
        Vector3 _right = Vector3.right * (_waveR * fSpeed);
        _base.transform.position = _base.StartPos + _right;
    }
    #endregion
}
