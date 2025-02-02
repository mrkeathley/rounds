using System;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable {

    public Item contents;
    public bool isOpened = false;
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;
    public Inventory playerInventory;

    void Start() {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if (!Input.GetKeyDown(KeyCode.Space) || !playerInRange) return;
        if (!isOpened) {
            Open();
        } else {
            Finish();
        }
    }

    public void Open() {
        dialogBox.SetActive(true);
        dialogText.text = contents.description;

        playerInventory.currentItem = contents;
        playerInventory.AddItem(contents);
        raiseItem.Raise();
        
        isOpened = true;
        context.Raise();
        
        anim.SetBool("opened", true);
    }

    public void Finish() {
        dialogBox.SetActive(false);
        raiseItem.Raise();
    }

    protected new void OnTriggerEnter2D(Collider2D other) {
        if (!isOpened) {
            base.OnTriggerEnter2D(other);
        }
    }

    protected new void OnTriggerExit2D(Collider2D other) {
        if(!isOpened) {
            base.OnTriggerExit2D(other);
        }
    }
}
