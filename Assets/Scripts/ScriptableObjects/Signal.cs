using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Signal : ScriptableObject
{
    public List<SignalListener> listenList = new List<SignalListener>();


    public void Raise()
    {
        for (int i = listenList.Count - 1; i >= 0; i--)
        {
            listenList[i].OnSignalRaised();
        }
    }


    public void ResigsterListener(SignalListener newListener)
    {
        listenList.Add(newListener);
    }


    public void DeregisterListener(SignalListener targetListener)
    {
        listenList.Remove(targetListener);
    }
}
