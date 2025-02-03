using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyState {
    idle,
    walk,
    attack,
    stagger,
    death,
}

public class Enemy : MonoBehaviour {
    public EnemyState currentState;
    public FloatValue maxHealth;
    private float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public GameObject deathEffect;

    protected void Start() {
        health = maxHealth.initialValue;
    }

    protected void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            DeathEffect();
            gameObject.SetActive(false);
        }
    }

    public void Knockback(Rigidbody2D myRigidbody, float knockbackTime, float damage) {
        if (currentState != EnemyState.stagger) {
            currentState = EnemyState.stagger;
            StartCoroutine(KnockbackCo(myRigidbody, knockbackTime));
            TakeDamage(damage);
        }
    }

    private IEnumerator KnockbackCo(Rigidbody2D myRigidbody, float knockbackTime) {
        yield return new WaitForSeconds(knockbackTime);
        myRigidbody.linearVelocity = Vector2.zero;
        currentState = EnemyState.idle;
    }

    private void DeathEffect() {
        if (deathEffect != null) {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }
}