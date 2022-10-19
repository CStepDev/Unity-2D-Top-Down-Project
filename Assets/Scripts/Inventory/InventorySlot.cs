using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Inventory Item UI Attributes")]
    [SerializeField] private Text itemNumberText;
    [SerializeField] private Image itemImage;

    [Header("Variables from the Item")]
    public Sprite itemSprite;
    public string itemDescription;
    public InventoryItem thisItem;
    public InventoryManager thisManager;


    public void Setup(InventoryItem newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;

        if (thisItem)
        {
            itemImage.sprite = thisItem.itemImage;
            itemNumberText.text = thisItem.numberHeld.ToString();
        }
    }


    public void ClickedOn()
    {
        if (thisItem)
        {
            thisManager.SetupDescriptionAndButton(thisItem.itemDescription, thisItem.usable, thisItem);
        }
    }
}
