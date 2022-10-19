// Author  : CS Dev
// Purpose : A script used for objects which need to persist between scenes, specifically a boolean value
//           in this case.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class BoolValue : ScriptableObject
{
    public bool initialValue;

    // Used the hide a public value in the inspector, disabled for debugging purposes
    //[HideInInspector]
    public bool runTimeValue;
}
