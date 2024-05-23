using UnityEngine;

public class Destroyer : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	#endregion
	
	#region Properties
	#endregion
	#endregion
	
	#region Methods
	private void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.GetComponent<Projectiles>() != null)
			Destroy(_other.gameObject);
		else if (_other.GetComponent<Loot>() != null)
			Destroy(_other.gameObject);
		else if (_other.GetComponent<Asteroids>() != null)
			Destroy(_other.gameObject);
	}
	#endregion
}
