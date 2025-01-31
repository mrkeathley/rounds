using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerState {
	walk,
	attack,
	interact,
	stagger,
	idle
}

public class PlayerMovement : MonoBehaviour {

	public PlayerState currentState;
	public float speed;
	private Rigidbody2D myRigidbody;
	private Vector3 change;
	private Animator animator;
	public FloatValue currentHealth;
	public Signal playerHealthSignal;

	void Start() {
		currentState = PlayerState.walk;
		animator = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();

		animator.SetFloat("moveX", 0);
		animator.SetFloat("moveY", -1);

	}

	void Update() {
		change = Vector3.zero;
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");

		if(Input.GetButtonDown("attack") && 
		   currentState != PlayerState.attack && 
		   currentState != PlayerState.stagger) {
			StartCoroutine(AttackCo());
		} else if (currentState == PlayerState.walk) {
			if (change != Vector3.zero) {
			animator.SetFloat("moveX", change.x);
			animator.SetFloat("moveY", change.y);
			animator.SetBool("moving", true);
			} else {
				animator.SetBool("moving", false);
			}
		}
	}

	void FixedUpdate() {
		if (currentState != PlayerState.stagger) {
			MoveCharacter();
		}
	}
	
	public void Knockback(float knockbackTime, float damage) {
		Debug.Log("Player Knockback");
		if (currentState != PlayerState.stagger) {
			
			currentHealth.runtimeValue -= damage;
			playerHealthSignal.Raise();
			
			if (currentHealth.runtimeValue > 0) {
				currentState = PlayerState.stagger;
				StartCoroutine(KnockbackCo(knockbackTime));
			} else {
				this.gameObject.SetActive(false);
			}
		}
	}
    
	private IEnumerator KnockbackCo(float knockbackTime) {
		yield return new WaitForSeconds(knockbackTime);
		myRigidbody.linearVelocity = Vector2.zero;
		currentState = PlayerState.walk;
	}

	private IEnumerator AttackCo() {
		animator.SetBool("attacking", true);
		currentState = PlayerState.attack;
		yield return null;
		animator.SetBool("attacking", false);
		yield return new WaitForSeconds(.33f);
		currentState = PlayerState.walk;
	}

	void MoveCharacter() {
		change.Normalize();
		myRigidbody.MovePosition(
			transform.position + change * speed * Time.deltaTime
		);		
	}
    
}
