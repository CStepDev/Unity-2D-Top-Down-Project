using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> playerItems = new List<Item>();
    public int numberOfKeys;
    public int numberOfCoins;
    public float currentMagic;
    public float maxMagic = 10;


    public bool CheckForItem(Item itemToCheckFor)
    {
        if (playerItems.Contains(itemToCheckFor))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void AddItem(Item itemToAdd)
    {
        // Is the item a key?
        if (itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            // Add to inventory only if we don't already have one
            if (!playerItems.Contains(itemToAdd))
            {
                playerItems.Add(itemToAdd);
            }
        }
    }


    public void ReduceMagic(float magicCost)
    {
        currentMagic -= magicCost;
    }


    public void OnEnable()
    {
        currentMagic = maxMagic;
    }
}
