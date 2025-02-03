using System;
using UnityEngine;

public class Heart : PowerUp {
    public FloatValue playerHealth;
    public float amountGained;
    public FloatValue heartContainers;

    protected override void PerformPowerUp() {
        playerHealth.runtimeValue += amountGained;
        if (playerHealth.runtimeValue > heartContainers.initialValue * 2) {
            playerHealth.runtimeValue = heartContainers.initialValue * 2;
        }
    }
}