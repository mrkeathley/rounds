using UnityEngine;

namespace Enemy {
    public class PatrolEnemy : Enemy {
        
        [Header("Patrol")]
        public Transform[] patrolPoints;
        public int currentPoint = 0;
        public Transform goal;
        public float roundingDistance;

        protected new void Start() {
            base.Start();
            ChangeState(EnemyState.Walk);
        }

        protected override void CheckDistance() {
            var distanceFromPlayer = Vector3.Distance(playerTransform.position, transform.position);
            if (IsPlayerInRange(distanceFromPlayer) && IsEnemyActivatable()) {
                MoveTowards(playerTransform);
            } else if (distanceFromPlayer > chaseRadius) {
                if (Vector3.Distance(transform.position, goal.position) <= roundingDistance) {
                    ChangeGoal();
                }
                else {
                    MoveTowards(goal);
                }
            }
        }

        private void ChangeGoal() {
            if (currentPoint == patrolPoints.Length - 1) {
                currentPoint = 0;
            }
            else {
                currentPoint++;
            }

            goal = patrolPoints[currentPoint];
        }
    }
}