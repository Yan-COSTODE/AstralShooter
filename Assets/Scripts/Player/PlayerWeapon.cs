using UnityEngine;

[CreateAssetMenu(fileName = "Player Weapon", menuName = "Custom/Player Weapon")]
public class PlayerWeapon : ScriptableObject
{
	#region Fields & Properties
	#region Fields
    [SerializeField] private Stat damage;
    [SerializeField] private Stat reload;
    [SerializeField] private GameObject visual;
	#endregion
	
	#region Properties
	#endregion
	#endregion
	
	#region Methods

    public void Setup(Transform _parent)
    {
        Instantiate(visual, _parent);
    }
    #endregion
}
