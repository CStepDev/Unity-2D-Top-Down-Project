using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool active;
    public BoolValue storedState;
    public Sprite stateSprite;
    private SpriteRenderer mySprite;

    public Door doorForSwitch;


    public void ActivateSwitch()
    {
        active = true;
        storedState.runTimeValue = active;
        doorForSwitch.OpenDoor();
        mySprite.sprite = stateSprite;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player")) && (!other.isTrigger))
        {
            ActivateSwitch();
        }
    }


    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        active = storedState.runTimeValue;

        if (active)
        {
            ActivateSwitch();
        }
    }
}
