using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct FloatRange
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private float fMin;
	[SerializeField] private float fMax;
	#endregion

	#region Properties
	public float Value => Random.Range(fMin, fMax);
	#endregion
	#endregion

	#region Methods
	public FloatRange(float _min, float _max)
	{
		fMin = _min;
		fMax = _max;
	}
	#endregion
}

[Serializable]
public struct IntRange
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private int iMin;
	[SerializeField] private int iMax;
	#endregion

	#region Properties
	public int Value => Random.Range(iMin, iMax + 1);
	#endregion
	#endregion

	#region Methods
	public IntRange(int _min, int _max)
	{
		iMin = _min;
		iMax = _max;
	}
	#endregion
}