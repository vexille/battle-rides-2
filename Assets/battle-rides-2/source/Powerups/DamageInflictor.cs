using UnityEngine;

namespace Luderia.BattleRides2.Powerups {
    public class DamageInflictor : MonoBehaviour {
        public float Damage = 10f;
        public bool DestroyOnHit = true;

        private void OnCollisionEnter(Collision collision) {
            if (DestroyOnHit) {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (DestroyOnHit) {
                Destroy(gameObject);
            }
        }
    }
}