using UnityEngine;

[CreateAssetMenu(fileName = "Kamikaze Pattern", menuName = "Custom/Pattern/Kamikaze Pattern")]
public class KamikazeMovementPattern : MovementPattern
{
    #region Fields & Properties
    #region Fields
    private Player player;
    #endregion

    #region Properties
    #endregion
    #endregion
    
    #region Methods
    public override void SetNextPosition(EnemyBase _base)
    {
        if (!player)
            player = Player.Instance;

        if (!player)
            return;

        Vector2 _playerPos = player.transform.position;
        Vector2 _position = _base.transform.position;
        Vector2 _newPosition = Vector2.MoveTowards(_base.transform.position, player.transform.position, fSpeed * Time.deltaTime);
        _base.transform.position = new Vector3(_newPosition.x, _newPosition.y, 0.0f);
        Vector2 _direction = _playerPos - _position;
        float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        _base.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, _angle - 90.0f));
        
    }
    #endregion
}
