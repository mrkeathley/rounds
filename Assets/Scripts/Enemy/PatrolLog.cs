using UnityEngine;

namespace Enemy {
    public class PatrolLog : PatrolEnemy {
        private static readonly int WakeUp = Animator.StringToHash("wakeUp");

        protected new void Start() {
            base.Start();
            anim.SetBool(WakeUp, true);
        }
    }
}