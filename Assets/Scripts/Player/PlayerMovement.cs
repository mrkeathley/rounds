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
	public VectorValue startingPosition;
	public Inventory inventory;
	public SpriteRenderer receivedItemSprite;

	void Start() {
		currentState = PlayerState.walk;
		animator = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();

		animator.SetFloat("moveX", 0);
		animator.SetFloat("moveY", -1);
		transform.position = startingPosition.initialValue;
	}

	void Update() {
		if (currentState == PlayerState.interact) return;
		
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
		if (currentState == PlayerState.interact) return;
		if (currentState == PlayerState.stagger) return;
		
		MoveCharacter();
	}
	
	public void Knockback(float knockbackTime, float damage) {
		if (currentState == PlayerState.stagger) return;
		currentHealth.runtimeValue -= damage;
		playerHealthSignal.Raise();
			
		if (currentHealth.runtimeValue > 0) {
			currentState = PlayerState.stagger;
			StartCoroutine(KnockbackCo(knockbackTime));
		} else {
			gameObject.SetActive(false);
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

		if (currentState != PlayerState.interact) {
			currentState = PlayerState.walk;
		}
	}

	public void ReceiveItem() {
		if (inventory.currentItem == null) return;
		if (currentState != PlayerState.interact) {
			animator.SetBool("receiveItem", true);
			currentState = PlayerState.interact;
			receivedItemSprite.sprite = inventory.currentItem.icon;
		} else {
			animator.SetBool("receiveItem", false);
			currentState = PlayerState.walk;
			receivedItemSprite.sprite = null;
			inventory.currentItem = null;
		}
	}

	void MoveCharacter() {
		change.Normalize();
		myRigidbody.MovePosition(
			transform.position + change * (speed * Time.deltaTime)
		);		
	}
    
}
