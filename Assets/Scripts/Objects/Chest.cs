using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    [Header("Contents")]
    public Item chestContents;

    [Header("Player Inventory Reference")]
    public Inventory playerInventory;

    [Header("Chest State")]
    public bool isOpen;
    public BoolValue storedState;
    private Animator anim;

    [Header("Dialogue and Signals")]
    public Signal playerObtainItem;
    public GameObject dialogueBox;
    public Text dialogue;


    public void OpenChest()
    {
        // Set up the dialogue box
        dialogueBox.SetActive(true);
        dialogue.text = chestContents.itemDescription;

        // Add item to the player inventory
        playerInventory.currentItem = chestContents;

        // Get the player to animate appropriately
        playerObtainItem.Raise();
        contextClueOn.Raise(); // Remove the context clue from atop the player

        // Animate the chest appropriately
        anim.SetBool("opened", true);

        // Set the chest to opened
        isOpen = true;
        storedState.runTimeValue = true;
    }


    public void OpenedChest()
    {
        // Turn the dialogue box off
        dialogueBox.SetActive(false);

        // Get the player to animate appropriately
        playerObtainItem.Raise();
    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player")) && (!other.isTrigger) && (!isOpen))
        {
            contextClueOn.Raise();
            isActive = true;
        }
    }


    public override void OnTriggerExit2D(Collider2D other)
    {
        if ((other.CompareTag("Player")) && (!other.isTrigger) && (!isOpen))
        {
            contextClueOn.Raise();
            isActive = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = storedState.runTimeValue;

        if (isOpen)
        {
            anim.SetBool("opened", true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Interact")) && (isActive))
        {
            if (!isOpen)
            {
                // Open the chest
                OpenChest();
            }
            else
            {
                OpenedChest();
            }
        }
    }
}
