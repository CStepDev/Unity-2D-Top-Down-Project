using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBottle : Pickup
{
    public float magicValue;
    public Inventory playerInventory;


    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInventory.currentMagic += magicValue;
            pickupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
