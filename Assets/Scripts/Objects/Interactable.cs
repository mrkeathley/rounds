using UnityEngine;

public class Interactable : MonoBehaviour {
    public bool contextVisible = false;
    public bool showContext = true;
    public bool playerInRange;
    public Signal context;

    public void DisableContext() {
        showContext = false;
        if (contextVisible) {
            context.Raise();
            contextVisible = false;
        }
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            playerInRange = true;
            if (showContext) {
                context.Raise();
                contextVisible = true;
            }
        }
    }

    protected void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player") || other.isTrigger) return;
        playerInRange = false;
        if (showContext) {
            context.Raise();
            contextVisible = false;
        }
    }
}