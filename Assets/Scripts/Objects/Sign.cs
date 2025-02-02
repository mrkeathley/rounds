using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sign : Interactable {

    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    
    void Update() {
        if (!Input.GetKeyDown(KeyCode.Space) || !playerInRange) return;
        if (dialogBox.activeInHierarchy) {
            dialogBox.SetActive(false);
        } else {
            dialogText.text = dialog;
            dialogBox.SetActive(true);
        }
    }
    
    protected new void OnTriggerExit2D(Collider2D other) {
        base.OnTriggerExit2D(other);
        if(other.CompareTag("Player") && !other.isTrigger) {
            dialogBox.SetActive(false);
        }
    }
}
