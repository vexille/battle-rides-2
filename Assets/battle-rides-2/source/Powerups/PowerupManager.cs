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

        [SerializeField]
        private PowerupPrefabs _prefabs;

        private PowerupSpawnPoint[] _spawnPoints;
        private ShuffleBag<PowerupSpawnPoint> _spawnShuffleBag;
        private ShuffleBag<PowerupType> _powerupShuffleBag;

        private int _activePowerups;

        public PowerupBalanceData BalanceData { get { return _balance; } }

        public PowerupPrefabs PowerupPrefabs { get { return _prefabs; } }

        public override void Initialize() {
            base.Initialize();

            _powerupShuffleBag = new ShuffleBag<PowerupType>();
            _spawnShuffleBag = new ShuffleBag<PowerupSpawnPoint>();
            _spawnPoints = GameObject.FindObjectsOfType<PowerupSpawnPoint>();
            if (_spawnPoints.Length == 0) {
                Debug.LogError("[PowerupManager] No powerup spawns in the scene!");
            }
            for (int i = 0; i < _spawnPoints.Length; i++) {
                _spawnPoints[i].SpawnPointIndex = i;
                _spawnShuffleBag.Add(_spawnPoints[i]);
            }

            InstanceBinder.Get<MessageRouter>().AddHandler<OnPowerupPickedUp>(message => {
                _activePowerups--;
                _spawnPoints[message.SpawnPointIndex].Active = false;
            });
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
            var powerup = _powerupShuffleBag.Next();
            var spawnPoint = _spawnShuffleBag.Next();

            // Ensure we get an inactive spot
            int retries = _spawnShuffleBag.Count;
            while (spawnPoint.Active && retries > 0) {
                spawnPoint = _spawnShuffleBag.Next();
                retries--;
            }
            if (spawnPoint.Active) {
                Debug.LogError("[PowerupManager] No available powerup spots found!");
                return;
            }

            spawnPoint.Active = true;

            var go = GameObject.Instantiate(_balance.PowerupDropPrefab, spawnPoint.transform.position, Quaternion.identity);
            var drop = go.GetComponent<PowerupDrop>();
            drop.Type = powerup;
            drop.SpawnPointIndex = spawnPoint.SpawnPointIndex;
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