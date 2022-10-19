using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public Signal objectSignal;
    public UnityEvent signalEvent;


    public void OnSignalRaised()
    {
        signalEvent.Invoke();
    }


    private void OnEnable()
    {
        objectSignal.ResigsterListener(this);
    }


    private void OnDisable()
    {
        objectSignal.DeregisterListener(this);
    }
}
