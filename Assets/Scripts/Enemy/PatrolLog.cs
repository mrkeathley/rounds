using UnityEngine;

public class PatrolLog : Log {
    
    public Transform[] patrolPoints;
    public int currentPoint = 0;
    public Transform goal;
    public float roundingDistance;

    protected new void Start() {
        base.Start();
        anim.SetBool("wakeUp", true);
        ChangeState(EnemyState.walk);
    }
    
    protected override void CheckDistance() {
        float currentDistance = Vector3.Distance(target.position, transform.position);

        if(currentDistance <= chaseRadius && currentDistance > attackRadius) {
            if ((currentState == EnemyState.idle || currentState == EnemyState.walk) && currentState != EnemyState.stagger) {
                MoveTowards(target);
            }
        } else if (currentDistance > chaseRadius) {
            if (Vector3.Distance(transform.position, goal.position) <= roundingDistance) {
                ChangeGoal();
            } else {
                MoveTowards(goal);
            }
        } 
    }

    private void MoveTowards(Transform towardsTarget) {
        Vector3 towards = Vector3.MoveTowards(transform.position, towardsTarget.position, moveSpeed * Time.deltaTime);
        changeAnim(towards - transform.position);
        logRigidbody.MovePosition(towards);
    }

    private void ChangeGoal() {
        if (currentPoint == patrolPoints.Length - 1) {
            currentPoint = 0;
            goal = patrolPoints[currentPoint];
        } else {
            currentPoint++;
            goal = patrolPoints[currentPoint];
        }
    }
}
