using Luderia.BattleRides2.Data;
using LuftSchloss;
using UnityEngine;

namespace Luderia.BattleRides2.Powerups {
    public class Mine : MonoBehaviour {
        public Collider MineCollider;

        public MineBalanceData BalanceData { get; set; }

        private void OnTriggerExit(Collider other) {
            Activate();
        }

        private void Activate() {
            Debug.Log("Activate!");
            MineCollider.isTrigger = false;

            var inflictor = gameObject.AddComponent<DamageInflictor>();
            inflictor.Damage = BalanceData.Damage;
            inflictor.DestroyOnHit = true;
        }

        private void OnDestroy() {
            InstanceBinder.Get<SoundController>().PlaySound("mine-hits-car");
        }
    }
}
