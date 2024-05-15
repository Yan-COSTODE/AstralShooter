using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private float iBase;
	private float fCurrent;
	private List<float> fFlatModifier = new();
	private List<float> fMultModifier = new();
	#endregion
	
	#region Properties
	public float Value => (iBase + CalculateFlat()) * CalculateMult();
	public float Current => fCurrent;
	#endregion
	#endregion
	
	#region Methods
	private void Start()
	{
		fCurrent = Value;
	}
	
	private float CalculateFlat()
	{
		float _fFinal = 0;

		for (int _i = 0; _i < fFlatModifier.Count; _i++)
			_fFinal += fFlatModifier[_i];

		return _fFinal;
	}
	
	private float CalculateMult()
    	{
    		float _fFinal = 1;
    
    		for (int _i = 0; _i < fMultModifier.Count; _i++)
			    _fFinal += fMultModifier[_i];
    
    		return _fFinal;
    	}
    #endregion
}
