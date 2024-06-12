using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyObjectSpawner : MonoBehaviour
{
    #region Fields & Properties

    #region Fields

    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int iMax;
    [SerializeField] private float fDelay;
    [SerializeField] private int iSpawnCount;
    [SerializeField] private List<Transform> sockets;
    private List<GameObject> currentObject = new();
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
        foreach (GameObject _object in currentObject)
        {
            if (_object)
                Destroy(_object);
        }

        currentObject.Clear();
    }

    private void CheckExist()
    {
        foreach (GameObject _object in currentObject.ToList())
        {
            if (!_object)
                currentObject.Remove(_object);
        }
    }

    private void Spawn()
    {
        if (!objectPrefab)
            return;

        CheckExist();

        if (currentObject.Count + iSpawnCount > iMax)
            return;

        for (int i = 0; i < iSpawnCount; i++)
        {
            Transform _socket = sockets[i % sockets.Count];
            GameObject _gameObject = Instantiate(objectPrefab, _socket.position, _socket.rotation);
            
            if(_gameObject.TryGetComponent(out Projectiles _projectiles))
                _projectiles.Setup(transform.GetComponent<EnemyBase>().Weapon);
            
            currentObject.Add(_gameObject);
        }
    }
    #endregion
}

