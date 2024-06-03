using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveEntry : MonoBehaviour
{
    #region Fields & Properties
    #region Fields
    [SerializeField] private string waveName;
    [SerializeField] private ulong iScore;
    [SerializeField] private List<GameObject> enemies;
    #endregion

    #region Properties
    public string WaveName => waveName;
    #endregion
    #endregion

    #region Methods
    private void Update()
    {
        foreach (GameObject _gameObject in enemies)
        {
            if (_gameObject)
                return;
        }
        
        EndOfWave();
    }

    private void EndOfWave()
    {
        Player.Instance.AddScore(iScore);
        Destroy(gameObject);
    }
    #endregion
}
