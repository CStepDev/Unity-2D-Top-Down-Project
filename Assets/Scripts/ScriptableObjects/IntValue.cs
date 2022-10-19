// Author  : CS Dev
// Purpose : A script used for objects which need to persist between scenes, specifically an int value
//           in this case, for things like the health of the Player.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mark a ScriptableObject-derived type to be automatically listed in the Assets/Create submenu, 
// so that instances of the type can be easily created and stored in the project as ".asset" files.
[CreateAssetMenu]
[System.Serializable]
public class IntValue : ScriptableObject
{
    public int initialValue;

    // Used the hide a public value in the inspector, disabled for debugging purposes
    //[HideInInspector]
    public int runTimeValue;
}