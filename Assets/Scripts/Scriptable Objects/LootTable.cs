using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class loot
{
	public PowerUp thisLoot;
	public int lootChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
	public loot[] loots;

	public PowerUp lootPowerUp()
	{
		int cumProb = 0;
		int currentProb = Random.Range(0, 100);
		for (int i = 0; i < loots.Length; i++)
		{
			cumProb += loots[i].lootChance;
			if (currentProb<= cumProb)
			{
				return loots[i].thisLoot; 
			}
		}
		return null;
	}
}
