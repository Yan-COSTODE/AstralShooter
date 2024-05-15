using UnityEngine;

public class SingletonTemplate<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Fields & Properties
    #region Fields
    private static T instance = null;
    #endregion
    
    #region Properties
    public static T Instance => instance;
    #endregion
    #endregion

    #region Methods
    protected virtual void Awake()
    {
        if (instance)
        {
            Debug.LogWarning($"More than one {typeof(T)} instance found");
            Destroy(this);
            return;
        }

        instance = this as T;
        name += " [MANAGER]";
    }
    #endregion
   
}