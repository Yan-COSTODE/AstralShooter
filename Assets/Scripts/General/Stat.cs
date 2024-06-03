using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct LifeSprite
{
	[SerializeField] private List<LifeSpriteEntry> entry;

	public void Init() => entry.Sort((_entry1, _entry2) => _entry2.Percent.CompareTo(_entry1.Percent));
	
	public Sprite GetSprite(float _percent)
	{
		foreach (LifeSpriteEntry _entry in entry)
		{
			if (_percent >= _entry.Percent)
				return _entry.Image;
		}

		return null;
	}
}

[Serializable]
public struct LifeSpriteEntry
{
	[SerializeField] private float fPercent;
	[SerializeField] private Sprite image;

	public float Percent => fPercent;
	public Sprite Image => image;
}

[Serializable]
public class Stat
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private float fMax;
	private float fCurrent = 0.0f;
	private List<float> fFlatModifier = new();
	private List<float> fMultModifier = new();
	#endregion
	
	#region Properties
	public float Current => fCurrent;
	public float Max => fMax * CalculateMult() + CalculateFlat();
	public float Percent => Current / Max;
	#endregion
	#endregion
	
	#region Methods
	public void Init()
	{
		fCurrent = fMax;
		fFlatModifier.Clear();
		fMultModifier.Clear();
	}

	public void ChangeMax(float _max)
	{
		if (_max < 0)
			_max = 0;
		float _change = _max / fMax;
		fMax = _max;
		fCurrent *= _change;
	}
	
	public void ChangeCurrent(float _current)
	{
		_current = Mathf.Clamp(_current, 0, Max);
		fCurrent = _current;
	}
	
	public void AddMax(float _add) => ChangeMax(fMax + _add);

	public void RemoveMax(float _remove) => ChangeMax(fMax - _remove);

	public void AddCurrent(float _add) => ChangeCurrent(fCurrent + _add);
	
	public void RemoveCurrent(float _remove) => ChangeCurrent(fCurrent - _remove);

	private void RefreshCurrent(float _oldMax)
	{
		if (_oldMax == 0.0f)
			return;
		
		float _change = fMax / _oldMax;
		fCurrent *= _change;
	}
	
	public void AddFlat(float _flat)
	{
		float _max = fMax;
		fFlatModifier.Add(_flat);
		RefreshCurrent(_max);
	}

	public void RemoveFlat(float _flat)
	{
		float _max = fMax;
		fFlatModifier.Remove(_flat);
		RefreshCurrent(_max);
	}
	
	public void AddMult(float _mult)
	{
		float _max = fMax;
		fMultModifier.Add(_mult);
		RefreshCurrent(_max);
	}

	public void RemoveMult(float _mult)
	{
		float _max = fMax;
		fMultModifier.Remove(_mult);
		RefreshCurrent(_max);
	}
	
	public float CalculateFlat()
	{
		float _fFinal = 0;

		foreach (float _t in fFlatModifier)
			_fFinal += _t;

		return _fFinal;
	}
	
	public float CalculateMult()
	{
    		float _fFinal = 1;
    
    		foreach (float _t in fMultModifier)
			    _fFinal += _t;

		    return _fFinal;
	}

	public void MaxOut()
	{
		fCurrent = Max;
	}
    #endregion
}
