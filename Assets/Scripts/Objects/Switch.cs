using System;
using UnityEngine;

public class Switch : MonoBehaviour {

    [Header("Activation")]
    public bool canBeDeactivated = true;
    public bool isActive;
    public BooleanValue storedValue;
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    
    [Header("Associated Door")]
    public Door switchDoor;

    private SpriteRenderer _spriteRenderer;
    
    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        isActive = storedValue.runtimeValue;
        if (isActive) {
            ActivateSwitch();
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player") || other.isTrigger) return;
        if (canBeDeactivated) {
            ToggleSwitch();
        } else if (!isActive) {
            ActivateSwitch();
        }
    }

    public void ToggleSwitch() {
        storedValue.runtimeValue = !storedValue.runtimeValue;
        isActive = storedValue.runtimeValue;
        _spriteRenderer.sprite = isActive ? activeSprite : inactiveSprite;
        switchDoor.ToggleDoor();
    }

    public void ActivateSwitch() {
        storedValue.runtimeValue = true;
        isActive = storedValue.runtimeValue;
        _spriteRenderer.sprite = activeSprite;
        switchDoor.Open();
    }
}
