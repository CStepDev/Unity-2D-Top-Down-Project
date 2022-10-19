using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public Pickup thisLoot;
    public int lootChance;
}


[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] objectLoot;

    public Pickup LootPowerup()
    {
        int cumulativeProb = 0;
        int currentProb = Random.Range(0, 100);

        for (int i = 0; i < objectLoot.Length; i++)
        {
            cumulativeProb += objectLoot[i].lootChance;

            if (currentProb <= cumulativeProb)
            {
                return objectLoot[i].thisLoot;
            }
        }

        return null;
    }
}
