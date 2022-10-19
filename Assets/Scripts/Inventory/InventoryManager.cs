using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Information")]
    public PlayerInventory currentPlayerInventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject useButton;
    public InventoryItem currentItem;


    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;

        if (buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }


    public void SetupDescriptionAndButton(string newDescription, bool canUse, InventoryItem newItem)
    {
        currentItem = newItem;
        descriptionText.text = newDescription;
        useButton.SetActive(canUse);
    }


    private void ClearInventorySlots()
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }


    public void UseButtonPressed()
    {
        if (currentItem)
        {
            currentItem.Use();
            ClearInventorySlots();
            MakeInventorySlots();

            if (currentItem.numberHeld == 0)
            {
                SetTextAndButton("", false);
            }     
        }
    }


    private void MakeInventorySlots()
    {
        if (currentPlayerInventory)
        {
            for (int i = 0; i < currentPlayerInventory.myInventory.Count; i++)
            {
                if (currentPlayerInventory.myInventory[i].numberHeld > 0 ||
                    currentPlayerInventory.myInventory[i].itemName == "Bottle")
                {
                    GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform);
                    InventorySlot newInvSlot = temp.GetComponent<InventorySlot>();

                    if (newInvSlot)
                    {
                        newInvSlot.Setup(currentPlayerInventory.myInventory[i], this);
                    }
                }
            }
        }
    }


    // Start is called before the first frame update
    void OnEnable()
    {
        ClearInventorySlots();
        MakeInventorySlots();
        SetTextAndButton("", false);
    }
}
