using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChangeMin; // The new minimum boundary of a room
    public Vector2 cameraChangeMax; // The new maximum boundary of a room
    public Vector2 playerChange; // Value used to bump player to next room
    private CameraMovement playerCamera; // Ref to CameraMovement on main cam

    public bool hasName; // Used to determine if place name has to appear
    public string roomName; // The name of the entered room
    public GameObject textUI;
    public Text placeText;
    private IEnumerator coroutine; // Stores the placeNameCo coroutine


    private IEnumerator placeNameCo(float time)
    {
        textUI.SetActive(true);
        placeText.text = roomName;

        yield return new WaitForSeconds(time);
        textUI.SetActive(false);
    }


    // Sets the minimum and maximum boundaries of the next room and bumps
    // the player into said next room if they collide with the object this
    // script is attached to
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player")) && (other.isTrigger))
        {
            playerCamera.minPosition = cameraChangeMin;
            playerCamera.maxPosition = cameraChangeMax;

            other.transform.position += new Vector3(playerChange.x,
                                                    playerChange.y,
                                                    0);

            if (hasName)
            {
                // A required check to avoid NullReferenceExceptions
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                
                coroutine = placeNameCo(4.0f);
                StartCoroutine(coroutine);
            }
        }
    }


    // Set references and initialize the coroutine to a known value
    void Start ()
    {
        playerCamera = Camera.main.GetComponent<CameraMovement>();
        coroutine = null;
    }
}
