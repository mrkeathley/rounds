using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy {
    public class TurretEnemy : Log {
        
        [Header("Turret")]
        public GameObject projectile;
        public int projectileCount = 2;
        public float projectileDelay = 0.5f;
        
        private float _projectileTimer;
        private bool _canFire;
        
        private List<GameObject> _projectiles = new();

        public void Update() {
            // Add Delay
            _projectileTimer += Time.deltaTime;
            _canFire = _projectileTimer >= projectileDelay;
            _projectileTimer = Mathf.Clamp(_projectileTimer, 0, projectileDelay);
            
            // Clean Old Projectiles from List
            for (var i = 0; i < _projectiles.Count; i++) {
                if (_projectiles[i]) continue;
                _projectiles.RemoveAt(i);
                i--;
            }
        }

        protected override void CheckDistance() {
            var distanceFromPlayer = Vector3.Distance(playerTransform.position, transform.position);
            if (IsPlayerInRange(distanceFromPlayer) && IsEnemyActivatable()) {
                if (!_canFire) return;
                FireProjectile();
                ChangeState(EnemyState.Walk);
                anim.SetBool(WakeUp, true);
            } else if (distanceFromPlayer > chaseRadius || !boundary.bounds.Contains(playerTransform.position)) {
                anim.SetBool(WakeUp, false);
            }
        }

        private void FireProjectile() {
            if (_projectiles.Count >= projectileCount) return;
            Debug.Log("Fire Projectile");
            var projectileDirection = playerTransform.position - transform.position;
            var current = Instantiate(projectile, transform.position, Quaternion.identity);
            current.GetComponent<RockProjectile>().FireProjectile(projectileDirection);
            _projectiles.Add(current);
        }
    
    }
}
