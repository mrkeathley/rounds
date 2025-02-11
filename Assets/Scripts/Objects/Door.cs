using UnityEngine;

public enum DoorType {
    Key,
    Button,
    Enemy
}

public class Door : Interactable {
    private static readonly int Opened = Animator.StringToHash("opened");

    [Header("Door Variables")] 
    public DoorType doorType;
    public bool open = false;
    
    [Header("References")]
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

    public void ToggleDoor() {
        if (open) {
            Close();
        }
        else {
            Open();
        }
    }

    public void Open() {
        anim.SetBool(Opened, true);
        open = true;
        physicsCollider.enabled = false;
        DisableContext();
    }

    public void Close() {
        open = false;
        anim.SetBool(Opened, false);
        physicsCollider.enabled = true;
        EnableContext();
    }
}