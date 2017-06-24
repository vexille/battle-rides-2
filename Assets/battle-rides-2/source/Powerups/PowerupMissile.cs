using Luderia.BattleRides2.Data;
using UnityEngine;

namespace Luderia.BattleRides2.Powerups {
    [RequireComponent(typeof(DamageInflictor))]
    public class PowerupMissile : MonoBehaviour {
        [SerializeField]
        public MissileBalanceData _balance;

        private Rigidbody _rigidbody;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start() {
            GetComponent<DamageInflictor>().Damage = _balance.Damage;
        }

        private void FixedUpdate() {
            Vector3 currentVelocity = _rigidbody.velocity;
            float currentSpeed = currentVelocity.magnitude;
            float speedChange = _balance.Speed - currentSpeed;
            if (speedChange <= 0f) {
                return;
            }

            _rigidbody.AddForce(transform.forward * speedChange, ForceMode.Impulse);
            Debug.Log("Speed: " + _rigidbody.velocity.magnitude);
        }
    }
}
