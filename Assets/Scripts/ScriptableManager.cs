using UnityEngine;

public enum EScriptable
{
    NONE,
    ROCKET,
    AUTO_CANNON,
    BIG_SPACE_GUN,
    MOVEMENT_CIRCULAR,
    MOVEMENT_WAVE,
    MOVEMENT_HORIZONTAL,
    KLAED_FIGHTER,
    KLAED_DREADNOUGHT,
    MOVEMENT_KAMIKAZE,
    KLAED_SCOUT,
    MOVEMENT_CIRCUIT_TOP,
    MOVEMENT_CIRCUIT_LEFT,
    MOVEMENT_CIRCUIT_RIGHT,
    NAIRAN_TORPEDO,
    NAIRAN_DREADNOUGHT,
    NAUTOLAN_FRIGATE,
    NAUTOLAN_DREADNOUGHT
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
    [SerializeField] private ScriptableObject movementHorizontal;
    [SerializeField] private ScriptableObject movementKamikaze;
    [SerializeField] private ScriptableObject klaedFighter;
    [SerializeField] private ScriptableObject klaedDreadnought;
    [SerializeField] private ScriptableObject klaedScout;
    [SerializeField] private ScriptableObject movementCircuitTop;
    [SerializeField] private ScriptableObject movementCircuitLeft;
    [SerializeField] private ScriptableObject movementCircuitRight;
    [SerializeField] private ScriptableObject nairanTorpedo;
    [SerializeField] private ScriptableObject nairanDreadnought;
    [SerializeField] private ScriptableObject nautolanFrigate;
    [SerializeField] private ScriptableObject nautolanDreadnought;
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
            case EScriptable.MOVEMENT_HORIZONTAL: _result = movementHorizontal;
                break;
            case EScriptable.MOVEMENT_KAMIKAZE: _result = movementKamikaze;
                break;
            case EScriptable.KLAED_FIGHTER: _result = klaedFighter;
                break;
            case EScriptable.KLAED_DREADNOUGHT: _result = klaedDreadnought;
                break;
            case EScriptable.KLAED_SCOUT: _result = klaedScout;
                break;
            case EScriptable.MOVEMENT_CIRCUIT_TOP: _result = movementCircuitTop;
                break;
            case EScriptable.MOVEMENT_CIRCUIT_LEFT: _result = movementCircuitLeft;
                break;
            case EScriptable.MOVEMENT_CIRCUIT_RIGHT: _result = movementCircuitRight;
                break;
            case EScriptable.NAIRAN_TORPEDO: _result = nairanTorpedo;
                break;
            case EScriptable.NAIRAN_DREADNOUGHT: _result = nairanDreadnought;
                break;
            case EScriptable.NAUTOLAN_FRIGATE: _result = nautolanFrigate;
                break;
            case EScriptable.NAUTOLAN_DREADNOUGHT: _result = nautolanDreadnought;
                break;
        }

        if (_result)
            return Instantiate(_result) as T;
        return _result as T;
    }
    #endregion
}
