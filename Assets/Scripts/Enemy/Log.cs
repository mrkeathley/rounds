using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Transactions;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Log : Enemy {

    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    protected Rigidbody2D logRigidbody;
    public Animator anim;
    
    protected new void Start() {
        base.Start();
        currentState = EnemyState.idle;
        target = GameObject.FindWithTag("Player").transform;
        logRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate() {
        CheckDistance();
    }

    protected virtual void CheckDistance() {
        float currentDistance = Vector3.Distance(target.position, transform.position);

        if(currentDistance <= chaseRadius && currentDistance > attackRadius) {
            if ((currentState == EnemyState.idle || currentState == EnemyState.walk) && currentState != EnemyState.stagger) {
                Vector3 towards = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnim(towards - transform.position);
                logRigidbody.MovePosition(towards);
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }
        } else if (currentDistance > chaseRadius) {
            anim.SetBool("wakeUp", false);
        } 
    }

    protected void SetAnimFloat(Vector2 setVector) {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    protected void changeAnim(Vector2 direction) {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            if (direction.x > 0) {
                SetAnimFloat(Vector2.right);
            } else if (direction.x < 0) {
                SetAnimFloat(Vector2.left);
            }
        } else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) {
            if (direction.y > 0) {
                SetAnimFloat(Vector2.up);
            } else if (direction.y < 0) {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    protected void ChangeState(EnemyState newState) {
        if (currentState != newState) {
            currentState = newState;
        }
    }
}
