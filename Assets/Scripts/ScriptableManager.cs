using UnityEngine;

public enum EScriptable
{
    NONE,
    ROCKET,
    AUTO_CANNON,
    BIG_SPACE_GUN,
    MOVEMENT_CIRCULAR,
    MOVEMENT_WAVE,
    KLAED_FIGHTER,
    KLAED_DREADNOUGHT
}

public class ScriptableManager : SingletonTemplate<ScriptableManager>
{
    #region Fields & Properties
    #region Fields
    [SerializeField] private ScriptableObject rocket;
    [SerializeField] private ScriptableObject autoCannon;
    [SerializeField] private ScriptableObject bigSpaceGun;
    [SerializeField] private ScriptableObject movementCircular;
    [SerializeField] private ScriptableObject movementWave;
    [SerializeField] private ScriptableObject klaedFighter;
    [SerializeField] private ScriptableObject klaedDreadnought;
    #endregion

    #region Properties
    #endregion
    #endregion
    
    #region Methods

    public T Get<T>(EScriptable _type) where T : ScriptableObject
    {
        ScriptableObject _result = null;
        
        switch (_type)
        {
            case EScriptable.ROCKET: _result = rocket;
                break;
            case EScriptable.AUTO_CANNON: _result = autoCannon;
                break;
            case EScriptable.BIG_SPACE_GUN: _result = bigSpaceGun;
                break;
            case EScriptable.MOVEMENT_CIRCULAR: _result = movementCircular;
                break;
            case EScriptable.MOVEMENT_WAVE: _result = movementWave;
                break;
            case EScriptable.KLAED_FIGHTER: _result = klaedFighter;
                break;
            case EScriptable.KLAED_DREADNOUGHT: _result = klaedDreadnought;
                break;
        }

        if (_result)
            return Instantiate(_result) as T;
        return _result as T;
    }
    #endregion
}
