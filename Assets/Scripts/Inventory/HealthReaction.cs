using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReaction : MonoBehaviour
{
    public IntValue playerHealth;
    public Signal healthSignal;


    public void Use(int amountToIncrease)
    {
        playerHealth.runTimeValue += amountToIncrease;
        healthSignal.Raise();
    }
}
