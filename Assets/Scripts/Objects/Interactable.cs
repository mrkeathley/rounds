using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Interactable : MonoBehaviour {
    
    [Header("Interactable")]
    private bool _contextVisible = false;
    public bool showContext = true;
    private bool _runtimeShowContext = true;
    public Signal contextSignal;
    protected bool playerInRange;

    public void Start() {
        _runtimeShowContext = showContext;
    }

    protected void DisableContext() {
        _runtimeShowContext = false;
        if (!_contextVisible) return;
        contextSignal.Raise();
        _contextVisible = false;
    }

    protected void EnableContext() {
        if(showContext) _runtimeShowContext = true;
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player") || other.isTrigger) return;
        playerInRange = true;
        if (!_runtimeShowContext) return;
        contextSignal.Raise();
        _contextVisible = true;
    }

    protected void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player") || other.isTrigger) return;
        playerInRange = false;
        if (!_runtimeShowContext) return;
        contextSignal.Raise();
        _contextVisible = false;
    }
}