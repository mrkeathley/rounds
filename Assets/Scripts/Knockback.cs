using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Knockback : MonoBehaviour {
    public float thrust;
    public float knockbackTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("breakable")) {
            other.GetComponent<Pot>().Smash();
        }
        else if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player")) {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null) {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (other.gameObject.CompareTag("enemy") && other.isTrigger) {
                    other.GetComponent<Enemy>().Knockback(hit, knockbackTime, damage);
                }
                else if (other.gameObject.CompareTag("Player") && other.isTrigger) {
                    other.GetComponent<PlayerMovement>().Knockback(knockbackTime, damage);
                }
            }
        }
    }
}