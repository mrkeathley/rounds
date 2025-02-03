using UnityEngine;

public enum DoorType {
    Key,
    Button,
    Enemy
}

public class Door : Interactable {
    [Header("Door Variables")] public DoorType doorType;
    public bool open = false;
    public Inventory playerInventory;
    public Animator anim;
    public BoxCollider2D physicsCollider;

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (playerInRange && doorType == DoorType.Key) {
                if (playerInventory.numberOfKeys > 0) {
                    playerInventory.numberOfKeys--;
                    Open();
                }
            }
        }
    }

    public void Open() {
        anim.SetBool("opened", true);
        open = true;
        physicsCollider.enabled = false;
        DisableContext();
    }

    public void Close() {
        open = false;
    }
}