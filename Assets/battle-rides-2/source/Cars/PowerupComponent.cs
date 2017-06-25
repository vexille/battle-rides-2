using Frictionless;
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
        private IEnumerator _shockCoroutine;
        private IEnumerator _nitroCoroutine;

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

                    InstanceBinder.Get<MessageRouter>().RaiseMessage(new PowerupSlotChanged {
                        CarIndex = Owner.CarIndex,
                        SlotIndex = i,
                        Value = type
                    });
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
                case PowerupType.Missile:       FireMissile(); break;
                case PowerupType.MachineGun:    StartFiringBullets(); break;
                case PowerupType.Shock:         StartShock(); break;
                case PowerupType.Landmine:      DeployMine(); break;
                case PowerupType.Nitrous:       StartNitro(); break;
                case PowerupType.Repair:        DoRepair(); break;
            }

            InstanceBinder.Get<MessageRouter>().RaiseMessage(new PowerupSlotChanged {
                CarIndex = Owner.CarIndex,
                SlotIndex = slotIndex,
                Value = PowerupType.None
            });

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

            Owner.View.FireFeedbackTrigger(CarView.MissileTrigger);
        }

        #region Bullets
        private void StartFiringBullets() {
            if (_bulletCoroutine != null) {
                Owner.View.StopCoroutine(_bulletCoroutine);
            }

            Owner.View.SetFeedbackBool(CarView.MachineGunTrigger, true);

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

            Owner.View.SetFeedbackBool(CarView.MachineGunTrigger, false);
        }
        #endregion

        #region Shock
        private void StartShock() {
            if (_shockCoroutine != null) {
                Owner.View.StopCoroutine(_shockCoroutine);
            }
            
            _shockCoroutine = ShockCoroutine();
            Owner.View.StartCoroutine(_shockCoroutine);
        }

        private IEnumerator ShockCoroutine() {
            Owner.SetShock(true);
            Owner.View.SetFeedbackBool(CarView.ShockTrigger, true);

            yield return new WaitForSeconds(_balance.ShockData.Duration);

            StopShock();
        }

        public void StopShock() {
            if (_shockCoroutine != null) {
                Owner.View.StopCoroutine(_shockCoroutine);
            }

            Owner.SetShock(false);
            Owner.View.SetFeedbackBool(CarView.ShockTrigger, false);
        }

        public void HandleShockHit(CarController target) {
            StopShock();
            // TODO: handle adjusting feedback to target
        }

        public void HandleShockTaken(CarController shocker) {
            Owner.TakeDamage(_balance.ShockData.Damage, true);
            // TODO: handle ajusting feedback to shocker
        }
        #endregion

        #region Nitro
        private void StartNitro() {
            if (_nitroCoroutine != null) {
                Owner.View.StopCoroutine(_nitroCoroutine);
            }

            _nitroCoroutine = NitroCoroutine();
            Owner.View.StartCoroutine(_nitroCoroutine);
        }

        private IEnumerator NitroCoroutine() {
            Owner.SetNitro(true, _balance.NitroData.Boost);
            Owner.View.SetFeedbackBool(CarView.NitroTrigger, true);

            yield return new WaitForSeconds(_balance.NitroData.Duration);

            Owner.View.SetFeedbackBool(CarView.NitroTrigger, false);
            Owner.SetNitro(false);
        }
        #endregion

        private void DeployMine() {
            var go = GameObject.Instantiate(_prefabs.MinePrefab, Owner.View.transform.position, Quaternion.identity);
            var mine = go.GetComponent<Mine>();
            mine.BalanceData = _balance.MineData;

            Owner.View.FireFeedbackTrigger(CarView.MineTrigger);
        }

        private void DoRepair() {
            Owner.Heal(_balance.RepairAmount);
            Owner.View.FireFeedbackTrigger(CarView.RepairTrigger);
        }
    }
}