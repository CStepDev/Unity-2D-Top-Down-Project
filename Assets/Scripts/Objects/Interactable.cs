using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Signal contextClueOn;
    public bool isActive;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player")) && (!other.isTrigger))
        {
            contextClueOn.Raise();
            isActive = true;
        }
    }


    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if ((other.CompareTag("Player")) && (!other.isTrigger))
        {
            contextClueOn.Raise();
            isActive = false;
        }
    }
}
