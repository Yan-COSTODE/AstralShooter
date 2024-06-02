using System;
using UnityEngine;
using System.Collections.Generic;

public class WeaponComp : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private Animator animator;
	[SerializeField] private List<Transform> sockets = new();
	#endregion
	
	#region Properties
	public List<Transform> Sockets => sockets;
	public Animator Animator => animator;
	#endregion
	#endregion

	#region Methods
	#endregion
}
