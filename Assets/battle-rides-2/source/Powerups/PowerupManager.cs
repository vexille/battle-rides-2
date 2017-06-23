using Frictionless;
using Luderia.BattleRides2.Data;
using Luderia.BattleRides2.Util;
using LuftSchloss;
using LuftSchloss.Core;
using LuftSchloss.Util;
using System.Collections;
using UnityEngine;

namespace Luderia.BattleRides2.Powerups {
    public class PowerupManager : LuftMonobehaviour {
        [SerializeField]
        private PowerupBalanceData _balance;

        private ShuffleBag<PowerupSpawnPoint> _spawnShuffleBag;
        private ShuffleBag<PowerupType> _powerupShuffleBag;

        private int _activePowerups;

        public override void Initialize() {
            base.Initialize();

            _powerupShuffleBag = new ShuffleBag<PowerupType>();
            _spawnShuffleBag = new ShuffleBag<PowerupSpawnPoint>();
            var spawnPoints = GameObject.FindObjectsOfType<PowerupSpawnPoint>();
            if (spawnPoints.Length == 0) {
                Debug.LogError("[PowerupManager] No powerup spawns in the scene!");
            }
            for (int i = 0; i < spawnPoints.Length; i++) {
                _spawnShuffleBag.Add(spawnPoints[i]);
            }

            InstanceBinder.Get<MessageRouter>().AddHandler<OnPowerupPickedUp>(message => _activePowerups--);
        }

        public override void LateInitialize() {
            base.LateInitialize();

            for (int i = 0; i < _balance.PowerupWeights.Count; i++) {
                _powerupShuffleBag.Add(_balance.PowerupWeights[i].Type, _balance.PowerupWeights[i].Weight);
            }
        }

        public void SetBalanceData(PowerupBalanceData data) {
            _balance = data;
        }

        public override void OnStartState() {
            base.OnStartState();

            InstanceBinder.Get<MonoProxy>().StartCoroutine(SpawnPowerupCoroutine());
        }

        private void SpawnPowerup() {
            var spawnPoint = _spawnShuffleBag.Next();
            var powerup = _powerupShuffleBag.Next();

            var go = GameObject.Instantiate(_balance.PowerupDropPrefab, spawnPoint.transform.position, Quaternion.identity);
            go.GetComponent<PowerupDrop>().Type = powerup;
            _activePowerups++;
        }

        private IEnumerator SpawnPowerupCoroutine() {
            while (true) {
                yield return new WaitForSeconds(_balance.PowerupSpawnInterval);

                if (_activePowerups < _balance.MaxActivePowerupPickups) {
                    SpawnPowerup();
                }
            }
        }
    }
}