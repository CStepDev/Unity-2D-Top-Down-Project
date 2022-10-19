using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Enemy[] roomEnemies;
    public Pot[] roomPots;

    public GameObject roomCamera;


    protected void ChangeActivation(Component objectComponent, bool setActive)
    {
        objectComponent.gameObject.SetActive(setActive);
    }


    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player")) && (!other.isTrigger))
        {
            for (int i = 0; i < roomEnemies.Length; i++)
            {
                ChangeActivation(roomEnemies[i], true);
            }

            for (int i = 0; i < roomPots.Length; i++)
            {
                ChangeActivation(roomPots[i], true);
            }

            roomCamera.SetActive(true);
        }
    }


    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if ((other.CompareTag("Player")) && (!other.isTrigger))
        {
            for (int i = 0; i < roomEnemies.Length; i++)
            {
                ChangeActivation(roomEnemies[i], false);
            }

            for (int i = 0; i < roomPots.Length; i++)
            {
                ChangeActivation(roomPots[i], false);
            }

            roomCamera.SetActive(false);
        }
    }
}
