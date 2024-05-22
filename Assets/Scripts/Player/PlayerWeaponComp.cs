using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerWeaponComp : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private List<Transform> sockets = new();
	#endregion
	
	#region Properties
	public List<Transform> Sockets => sockets;
	#endregion

	#endregion

	#region Methods
	#endregion
}
