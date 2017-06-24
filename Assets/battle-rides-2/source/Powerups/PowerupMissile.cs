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

        private void Update() {
            _rigidbody.AddForce(transform.forward * _balance.Speed, ForceMode.Impulse);
        }
    }
}
