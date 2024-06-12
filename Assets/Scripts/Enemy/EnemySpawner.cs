using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Fields & Properties
    #region Fields
    [SerializeField] private EnemyBase enemyPrefab;
    [SerializeField] private int iMax;
    [SerializeField] private float fDelay;
    [SerializeField] private int iSpawnCount;
    [SerializeField] private List<Transform> sockets;
    private List<EnemyBase> currentEnemy = new();
    #endregion

    #region Properties
    #endregion
    #endregion
    
    #region Methods
    private void Start()
    {
        if (sockets.Count == 0)
            return;
        
        if (iSpawnCount > iMax)
            iSpawnCount = iMax;
        
        InvokeRepeating(nameof(Spawn), fDelay, fDelay);
    }

    public void Destroy()
    {
        foreach (EnemyBase _enemy in currentEnemy)
        {
            if (_enemy)
                _enemy.Die(false);
        }

        currentEnemy.Clear();
    }

    private void CheckExist()
    {
        foreach (EnemyBase _enemy in currentEnemy.ToList())
        {
            if (!_enemy)
                currentEnemy.Remove(_enemy);
        }
    }
    
    private void Spawn()
    {
        if (!enemyPrefab)
            return;
        
        CheckExist();
        
        if (currentEnemy.Count + iSpawnCount > iMax)
            return;

        for (int i = 0; i < iSpawnCount; i++)
        {
            Transform _socket = sockets[i % sockets.Count];
            EnemyBase _enemy = Instantiate(enemyPrefab, _socket.position, _socket.rotation, _socket);
            currentEnemy.Add(_enemy);
        }
    }
    #endregion
}
