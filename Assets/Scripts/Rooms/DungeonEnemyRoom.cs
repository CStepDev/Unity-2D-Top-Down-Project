using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    public Door[] roomDoors;


    public void CloseDoors()
    {
        for (int i = 0; i < roomDoors.Length; i++)
        {
            roomDoors[i].CloseDoor();
        }
    }


    public void OpenDoors()
    {
        for (int i = 0; i < roomDoors.Length; i++)
        {
            roomDoors[i].OpenDoor();
        }
    }


    public void CheckEnemies()
    {
        bool enemiesRemain = false;

        for (int i = 0; i < roomEnemies.Length; i++)
        {
            if ((roomEnemies[i].gameObject.activeInHierarchy) && (i < roomEnemies.Length - 1))
            {
                enemiesRemain = true;
            }

            if (!enemiesRemain)
            {
                OpenDoors();
            }
        }
    }


    public override void OnTriggerEnter2D(Collider2D other)
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

            CloseDoors();

            roomCamera.SetActive(true);
        }    
    }


    public override void OnTriggerExit2D(Collider2D other)
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
