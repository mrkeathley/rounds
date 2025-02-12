using UnityEngine;

public class Projectile : MonoBehaviour {
    
    [Header("Stats")]
    public float moveSpeed;
    public int damage;
    public float lifeTime;
    private float _timeAlive;
    
    public Vector2 directionToMove;
    public Rigidbody2D projectileRigidbody;
    
    protected void Start() {
        _timeAlive = 0;
    }
    
    protected void Update() {
        _timeAlive += Time.deltaTime;
        if (_timeAlive >= lifeTime) {
            Destroy(gameObject);
        }
    }

    public void FireProjectile(Vector2 direction) {
        directionToMove = direction;
        gameObject.SetActive(true);
        projectileRigidbody.linearVelocity = directionToMove * moveSpeed;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
