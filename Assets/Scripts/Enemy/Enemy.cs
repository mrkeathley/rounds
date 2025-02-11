using System.Collections;
using UnityEngine;

namespace Enemy {
    public enum EnemyState {
        Idle,
        Walk,
        Attack,
        Stagger,
        Death,
    }

    public class Enemy : MonoBehaviour {
    
        private static readonly int MoveX = Animator.StringToHash("moveX");
        private static readonly int MoveY = Animator.StringToHash("moveY");
    
        public EnemyState currentState;
        protected Animator anim;
    
        [Header("Stats")]
        public FloatValue maxHealth;
        private float _health;
        public int baseAttack;
        public float moveSpeed;
        public float chaseRadius;
        public float attackRadius;
    
        [Header("Effects")]
        public GameObject deathEffect;

        [Header("Settings")]
        public string enemyName;
        protected Rigidbody2D enemyRigidBody;
        public Collider2D boundary;
    
        protected Transform playerTransform;

        protected void Start() {
            _health = maxHealth.initialValue;
            currentState = EnemyState.Idle;
            anim = GetComponent<Animator>();
            enemyRigidBody = GetComponent<Rigidbody2D>();
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
        
        protected void FixedUpdate() {
            CheckDistance();
        }
    
        protected void ChangeState(EnemyState newState) {
            currentState = newState;
        }
    
        protected virtual void CheckDistance() {
            var distanceFromPlayer = Vector3.Distance(playerTransform.position, transform.position);
            if (IsPlayerInRange(distanceFromPlayer) && IsEnemyActivatable()) {
                MoveTowards(playerTransform);
            }
        }

        protected bool IsEnemyActivatable() {
            return currentState is EnemyState.Idle or EnemyState.Walk && currentState != EnemyState.Stagger;
        }

        protected bool IsPlayerInRange(float distanceFromPlayer) {
            return distanceFromPlayer <= chaseRadius && distanceFromPlayer > attackRadius && boundary.bounds.Contains(playerTransform.position);
        }

        protected void MoveTowards(Transform target) {
            var towards = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            ChangeAnimation(towards - transform.position);
            enemyRigidBody.MovePosition(towards);
            ChangeState(EnemyState.Walk);
        }
    
        protected void SetAnimationFloat(Vector2 setVector) {
            anim.SetFloat(MoveX, setVector.x);
            anim.SetFloat(MoveY, setVector.y);
        }

        protected void ChangeAnimation(Vector2 direction) {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
                switch (direction.x) {
                    case > 0:
                        SetAnimationFloat(Vector2.right);
                        break;
                    case < 0:
                        SetAnimationFloat(Vector2.left);
                        break;
                }
            }
            else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) {
                switch (direction.y) {
                    case > 0:
                        SetAnimationFloat(Vector2.up);
                        break;
                    case < 0:
                        SetAnimationFloat(Vector2.down);
                        break;
                }
            }
        }

        protected void TakeDamage(float damage) {
            _health -= damage;
            if (!(_health <= 0)) return;
            DeathEffect();
            gameObject.SetActive(false);
        }

        public void Knockback(Rigidbody2D myRigidbody, float knockbackTime, float damage) {
            if (currentState == EnemyState.Stagger) return;
            currentState = EnemyState.Stagger;
            StartCoroutine(KnockbackCo(myRigidbody, knockbackTime));
            TakeDamage(damage);
        }

        private IEnumerator KnockbackCo(Rigidbody2D myRigidbody, float knockbackTime) {
            yield return new WaitForSeconds(knockbackTime);
            myRigidbody.linearVelocity = Vector2.zero;
            currentState = EnemyState.Idle;
        }

        private void DeathEffect() {
            if (deathEffect == null) return;
            var effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }
}