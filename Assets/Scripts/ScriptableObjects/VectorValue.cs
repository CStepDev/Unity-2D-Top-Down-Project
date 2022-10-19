using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class VectorValue : ScriptableObject
{
    public Vector2 initialValue;

    [HideInInspector]
    public Vector2 runTimeValue;
}
