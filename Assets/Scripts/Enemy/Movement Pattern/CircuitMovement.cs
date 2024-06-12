using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Circuit Pattern", menuName = "Custom/Pattern/Circuit Pattern")]
public class CircuitMovement : MovementPattern
{
    #region Fields & Properties
    #region Fields
    [SerializeField] private List<Vector3> waypoints;
    [SerializeField] private float fDistanceThreshold = 0.5f;
    [SerializeField] private float fRotationSpeed = 5.0f;
    private int iIndex = 0;
    #endregion

    #region Properties
    #endregion
    #endregion
    
    #region Methods
    public override void SetNextPosition(EnemyBase _base)
    {
        if (waypoints.Count == 0)
            return;

        Vector3 _targetPosition = waypoints[iIndex];
        Vector3 _direction = (_targetPosition - _base.transform.position).normalized;
        _base.transform.position += _direction * (fSpeed * Time.deltaTime);

        float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        Quaternion _lookRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, _angle - 90.0f));
        _base.transform.rotation = Quaternion.Slerp(_base.transform.rotation, _lookRotation, fRotationSpeed * Time.deltaTime);

        if (Vector3.Distance(_base.transform.position, _targetPosition) < fDistanceThreshold)
            iIndex = (iIndex + 1) % waypoints.Count;
    }
    #endregion

}
