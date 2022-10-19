using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Pickup
{
    public Inventory playerInventory;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player")) && (!other.isTrigger))
        {
            playerInventory.numberOfCoins += 1;
            pickupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
