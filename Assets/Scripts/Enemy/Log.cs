using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Enemy {
    public class Log : Enemy {
        protected static readonly int WakeUp = Animator.StringToHash("wakeUp");
    
        protected override void CheckDistance() {
            var distanceFromPlayer = Vector3.Distance(playerTransform.position, transform.position);
            if (IsPlayerInRange(distanceFromPlayer) && IsEnemyActivatable()) {
                MoveTowards(playerTransform);
                anim.SetBool(WakeUp, true);
            } else if (distanceFromPlayer > chaseRadius || !boundary.bounds.Contains(playerTransform.position)) {
                anim.SetBool(WakeUp, false);
            }
        }

    }
}