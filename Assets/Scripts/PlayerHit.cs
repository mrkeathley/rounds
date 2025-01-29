using UnityEngine;

public class PlayerHit : MonoBehaviour {
    
    void Start() {
        
    }

    void Update() {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("breakable")) {
            other.GetComponent<Pot>().Smash();
        }
    }
}
