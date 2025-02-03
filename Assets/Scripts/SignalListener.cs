using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class SignalListener : MonoBehaviour {
    public Signal signal;
    public UnityEvent onSignal;

    public void OnSignalRaised() {
        onSignal.Invoke();
    }

    private void OnEnable() {
        if (signal != null) {
            signal.RegisterListener(this);
        }
    }

    private void OnDisable() {
        if (signal != null) {
            signal.UnregisterListener(this);
        }
    }
}