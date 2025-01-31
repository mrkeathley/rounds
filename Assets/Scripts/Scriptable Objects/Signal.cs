using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class Signal : ScriptableObject {
    
    public List<SignalListener> listeners = new List<SignalListener>();

    public void Raise() {
        for (int i = listeners.Count - 1; i >= 0; i--) {
            listeners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener listener) {
        listeners.Add(listener);
    }

    public void UnregisterListener(SignalListener listener) {
        listeners.Remove(listener);
    }
    
}
