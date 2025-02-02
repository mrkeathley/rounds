using UnityEngine;

public class Interactable : MonoBehaviour {
    
    public bool playerInRange;
    public Signal context;
  
    protected void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger) {
            playerInRange = true;
            context.Raise();
        }
    }

    protected void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player") || other.isTrigger) return;
        playerInRange = false;
        context.Raise();
    }
}
