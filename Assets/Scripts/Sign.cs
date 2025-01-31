using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sign : MonoBehaviour {

    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;


    void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange) {
            if (dialogBox.activeInHierarchy) {
                dialogBox.SetActive(false);
            } else {
                dialogText.text = dialog;
                dialogBox.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
