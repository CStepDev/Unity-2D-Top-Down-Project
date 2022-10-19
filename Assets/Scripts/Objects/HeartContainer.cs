using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainer : Pickup
{
    public IntValue playerHealth;
    public IntValue currentHeartContainers;


    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentHeartContainers.runTimeValue += 1;
            playerHealth.runTimeValue = currentHeartContainers.runTimeValue;
            pickupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
