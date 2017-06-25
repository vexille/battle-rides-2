using Luderia.BattleRides2.Data;
using Luderia.BattleRides2.Powerups;
using LuftSchloss;
using LuftSchloss.Core;
using UnityEngine;

namespace Luderia.BattleRides2.Cars {
    public class PowerupComponent : LuftController {
        private PowerupType[] _powerupSlots;
        private PowerupPrefabs _prefabs;
        private PowerupBalanceData _balance;

        public CarController Owner { get; set; }

        public override void Initialize() {
            base.Initialize();

            _powerupSlots = new PowerupType[PowerupBalanceData.MaxPowerupSlots];

            var powerupManager = InstanceBinder.Get<PowerupManager>();
            _prefabs = powerupManager.PowerupPrefabs;
            _balance = powerupManager.BalanceData;
        }

        public bool AddPowerup(PowerupType type) {
            for (int i = 0; i < _powerupSlots.Length; i++) {
                if (_powerupSlots[i] == PowerupType.None) {
                    _powerupSlots[i] = type;
                    UnityEngine.Debug.Log("Add powerup: " + type + " in slot " + i);
                    return true;
                }
            }

            return false;
        }

        public void UsePowerup(int slotIndex) {
            UnityEngine.Debug.Log("Use slot: " + slotIndex + " > " + _powerupSlots[slotIndex]);
            var powerup = _powerupSlots[slotIndex];
            if (powerup == PowerupType.None) {
                return;
            }

            switch (powerup) {
                case PowerupType.Missile: SpawnMissile(); break;
                case PowerupType.MachineGun: break;
                case PowerupType.Shock: break;
                case PowerupType.Landmine: break;
                case PowerupType.Nitrous: break;
                case PowerupType.Repair: break;
            }

            _powerupSlots[slotIndex] = PowerupType.None;
        }

        private void SpawnMissile() {
            var go = GameObject.Instantiate(
                _prefabs.MissilePrefab, 
                Owner.View.MissilePivot.position, 
                Owner.View.transform.localRotation);

            var inflictor = go.AddComponent<MovingDamageInflictor>();
            inflictor.Damage = _balance.MissileDamage;
            inflictor.DesiredSpeed = _balance.MissileSpeed;
        }
    }
}