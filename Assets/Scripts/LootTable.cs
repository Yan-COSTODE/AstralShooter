using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[Serializable]
public class LootTable
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private List<LootElem> loots = new();
	#endregion
	
	#region Properties
	public List<LootElem> Loots => loots;
	#endregion
	#endregion
	
	#region Methods
	public void Init()
	{
		loots.Sort((_loot1, _loot2) => _loot1.weight.CompareTo(_loot2.weight));
	}
	
	public void Roll(Transform _parent, float _luck = 1.0f)
	{
		float _totalWeight = 0.0f;
		
		foreach (LootElem _loot in loots)
			_totalWeight += _loot.weight * _luck;
		
		float _rand = Random.Range(0.0f, _totalWeight);
		float _cumulativeWeight = 0.0f;

		foreach (LootElem _loot in loots)
		{
			_cumulativeWeight += _loot.weight * _luck;

			if (_rand > _cumulativeWeight)
				continue;

			if (!_loot.loot)
				continue;
			
			UnityEngine.Object.Instantiate(_loot.loot, _parent.position, Quaternion.identity);
			return;
		}
	}
    #endregion
}

[Serializable]
public struct LootElem
{
	public Loot loot;
	public float weight;
}