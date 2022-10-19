using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    KEY,
    ENEMY,
    BUTTON
}

public class Door : Interactable
{
    [Header("Door Variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D doorCollider;


    public void OpenDoor()
    {
        // Disable the sprite renderer for the door
        doorSprite.enabled = false;
        
        // Stop the player colliding with it
        doorCollider.enabled = false;

        // Flag the boolean for the door being open
        open = true;
    }


    public void CloseDoor()
    {
        // Disable the sprite renderer for the door
        doorSprite.enabled = true;

        // Stop the player colliding with it
        doorCollider.enabled = true;

        // Flag the boolean for the door being open
        open = false;
    }


    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if ((isActive) && (thisDoorType == DoorType.KEY))
            {
                if (playerInventory.numberOfKeys > 0)
                {
                    playerInventory.numberOfKeys--;
                    OpenDoor();
                }
            }
        }
    }
}
