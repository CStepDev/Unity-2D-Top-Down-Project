// Author  : CS Dev
// Purpose : This script controls the movement of the camera within a room
//           based on variables fed in by public components, which are then
//           changed at runtime through RoomMove.cs.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Position Variables")]
    public Transform playerPosition; // Current player position
    public float smoothing; // How smoothly the camera will move around
    public Vector2 minPosition; // The minimum bounds of current room
    public Vector2 maxPosition; // The maximum bounds of current room

    [Header("Camera Position Reset")]
    public VectorValue camMin;
    public VectorValue camMax;

    [Header("Camera Animator")]
    public Animator anim;

    private IEnumerator kickCoroutine; // Stores KickCo for execution


    public IEnumerator KickCo()
    {
        yield return null;
        anim.SetBool("kickActive", false);
    }


    public void BeginKick()
    {
        anim.SetBool("kickActive", true);
        kickCoroutine = KickCo();
        StartCoroutine(kickCoroutine);
    }


    // Shift the camera to the starting position of the player when a scene
    // loads, at least initially
    void Start()
    {
        transform.position = new Vector3(playerPosition.position.x,
                                         playerPosition.position.y,
                                         transform.position.z);

        // Adjust the camera position on each scene load when Start is ran in case of entries from other scenes
        // away from the initial starting point of the loaded scene
        minPosition = camMin.runTimeValue;
        maxPosition = camMax.runTimeValue;

        anim = GetComponent<Animator>();
    }


    // This moves the camera using the last update function of the frame,
    // using the new position of the player within said frame and ensuring
    // the camera will only snap to it if it's within certain established
    // room bounds
    void LateUpdate ()
    {
        Vector3 targetPosition = new Vector3(playerPosition.position.x,
                                             playerPosition.position.y,
                                             transform.position.z);

        targetPosition.x = Mathf.Clamp(targetPosition.x,
                                       minPosition.x,
                                       maxPosition.x);

        targetPosition.y = Mathf.Clamp(targetPosition.y,
                                       minPosition.y,
                                       maxPosition.y);

        if (transform.position != targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position,
                                              targetPosition,
                                              smoothing);
        }
	}
}
