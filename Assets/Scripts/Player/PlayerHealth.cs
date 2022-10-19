using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : GenericHealth
{
    [SerializeField] private Signal healthSignal;

    public override void Damage(int amount)
    {
        base.Damage(amount);
        maxHealth.runTimeValue = currentHealth;
        healthSignal.Raise();
    }
}
