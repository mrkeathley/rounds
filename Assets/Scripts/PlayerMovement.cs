using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerState {
	walk,
	attack,
	interact
}

public class PlayerMovement : MonoBehaviour {

	public PlayerState currentState;
	public float speed;
	private Rigidbody2D myRigidbody;
	private Vector3 change;
	private Animator animator;

	void Start() {
		currentState = PlayerState.walk;
		animator = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();
	}

	void Update() {
		change = Vector3.zero;
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");

		if(Input.GetButtonDown("attack") && currentState != PlayerState.attack) {
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
		MoveCharacter();
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
		myRigidbody.MovePosition(
			transform.position + change * speed * Time.deltaTime
		);		
	}
    
}
