using UnityEngine;

public class PowerUp : MonoBehaviour {
    public Signal powerUpSignal;

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            PerformPowerUp();
            powerUpSignal.Raise();
            Destroy(gameObject);
        }
    }

    protected virtual void PerformPowerUp() { }
}