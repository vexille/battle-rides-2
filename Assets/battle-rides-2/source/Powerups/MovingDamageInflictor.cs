using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luderia.BattleRides2.Powerups {
    public class MovingDamageInflictor : DamageInflictor {
        public float DesiredSpeed;

        private Rigidbody _rigidbody;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate() {
            Vector3 currentVelocity = _rigidbody.velocity;
            float currentSpeed = currentVelocity.magnitude;
            float speedChange = DesiredSpeed - currentSpeed;
            if (speedChange <= 0f) {
                return;
            }

            _rigidbody.AddForce(transform.forward * speedChange * _rigidbody.mass, ForceMode.Impulse);
        }
    }
}
