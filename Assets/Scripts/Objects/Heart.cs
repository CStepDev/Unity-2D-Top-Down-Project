using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Pickup
{
    public IntValue playerHealth;
    public IntValue heartContainers;
    public int amountToIncrease;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player")) && (!other.isTrigger))
        {
            playerHealth.runTimeValue += amountToIncrease;

            if (playerHealth.runTimeValue > heartContainers.runTimeValue)
            {
                playerHealth.runTimeValue = heartContainers.runTimeValue;
            }

            pickupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
