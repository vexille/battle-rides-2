using Luderia.BattleRides2.Data;
using Luderia.BattleRides2.Powerups;
using LuftSchloss;
using LuftSchloss.Core;
using System.Collections;
using UnityEngine;

namespace Luderia.BattleRides2.Cars {
    public class PowerupComponent : LuftController {
        private PowerupType[] _powerupSlots;
        private PowerupPrefabs _prefabs;
        private PowerupBalanceData _balance;

        private IEnumerator _bulletCoroutine;

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
                case PowerupType.Missile: FireMissile(); break;
                case PowerupType.MachineGun: StartFiringBullets(); break;
                case PowerupType.Shock: break;
                case PowerupType.Landmine: break;
                case PowerupType.Nitrous: break;
                case PowerupType.Repair: break;
            }

            _powerupSlots[slotIndex] = PowerupType.None;
        }

        private void FireMissile() {
            var go = GameObject.Instantiate(
                _prefabs.MissilePrefab, 
                Owner.View.MissilePivot.position, 
                Owner.View.transform.localRotation);

            var inflictor = go.AddComponent<MovingDamageInflictor>();
            inflictor.Damage = _balance.MissileData.Damage;
            inflictor.DesiredSpeed = _balance.MissileData.Speed;
            inflictor.DestroyOnHit = true;
        }

        private void StartFiringBullets() {
            if (_bulletCoroutine != null) {
                Owner.View.StopCoroutine(_bulletCoroutine);
            }

            _bulletCoroutine = BulletCoroutine();
            Owner.View.StartCoroutine(_bulletCoroutine);
        }

        private void FireBullets() {
            var b1Pos = (Owner.View.MissilePivot.right * _balance.BulletData.DistanceBetweenBullets) + Owner.View.MissilePivot.position;
            var b1 = GameObject.Instantiate(_prefabs.BulletPrefab, b1Pos, Owner.View.transform.localRotation);

            var b2Pos = (Owner.View.MissilePivot.right * -_balance.BulletData.DistanceBetweenBullets) + Owner.View.MissilePivot.position;
            var b2 = GameObject.Instantiate(_prefabs.BulletPrefab, b2Pos, Owner.View.transform.localRotation);

            SetupBullet(b1);
            SetupBullet(b2);
        }

        private void SetupBullet(GameObject go) {
            var inflictor = go.AddComponent<MovingDamageInflictor>();
            inflictor.Damage = _balance.BulletData.Damage;
            inflictor.DesiredSpeed = _balance.BulletData.Speed;
            inflictor.DestroyOnHit = true;
        }

        private IEnumerator BulletCoroutine() {
            for (int i = 0; i < _balance.BulletData.BulletPairsFired; i++) {
                FireBullets();
                yield return new WaitForSeconds(_balance.BulletData.ShotsInterval);
            }
        }
    }
}