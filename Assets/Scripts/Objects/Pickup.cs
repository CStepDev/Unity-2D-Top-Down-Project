// Written by CS_Dev
// Description : This is the pickup class which all game pickups inherit from. I decided to make it an abstract class
//               on purpose as a deviation from the tutorial because all pickups in a game like this need to have an
//               implementation of OnTriggerEnter2D to provide collider functionality. This way, the need to provide
//               said functionality in an inheriting class is enforced, rather than leaving the potential for pickups
//               to behave inappropriately.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public Signal pickupSignal;

    // Enforce the implementation of this function so the player can walk over any pickup to acquire it
    public abstract void OnTriggerEnter2D(Collider2D other);
}
