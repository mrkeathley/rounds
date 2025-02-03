using UnityEngine;

public class Coin : PowerUp {
    public Inventory playerInventory;

    void Start() {
        powerUpSignal.Raise();
    }

    protected override void PerformPowerUp() {
        playerInventory.coins++;
    }
}