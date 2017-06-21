using Luderia.BattleRides2.Cars;
using Luderia.BattleRides2.Data;
using LuftSchloss;
using LuftSchloss.Util;
using System.Collections.Generic;
using UnityEngine;

namespace Luderia.BattleRides2.States {
    public class GameState : AppState {
        protected override IBindingStrategy CreateBinding() {
            return new GameStateBindings();
        }

        [SerializeField]
        private CarDataList _carDataList;
        private ShuffleBag<CarData> _carShuffleBag;

        // Car input controller?
        private List<CarController> _allCars;

        public override void Initialize() {
            base.Initialize();

            _allCars = new List<CarController>();
            _carShuffleBag = new ShuffleBag<CarData>();
            for (int i = 0; i < _carDataList.CarList.Count; i++) {
                _carShuffleBag.Add(_carDataList.CarList[i]);
            }

            CarSpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<CarSpawnPoint>();
            for (int i = 0; i < spawnPoints.Length; i++) {
                var currentPoint = spawnPoints[i];
                CreateCarAt(currentPoint, i);

                GameObject.Destroy(currentPoint.gameObject);
            }
        }

        private void CreateCarAt(CarSpawnPoint spawnPoint, int index) {
            var carController = AddChild<CarController>();
            carController.InitializeCar(index, _carShuffleBag.Next(), spawnPoint.transform.position, spawnPoint.transform.rotation);
            carController.OnHealthChanged += (i, health) => OnCarHealthChanged(i, health);
            carController.OnHealthDepleted += (i) => OnCarHealthDepleted(i);
            AddChild(carController.View);

            _allCars.Add(carController);
        }

        public override void OnUpdate() {
            if (_allCars.Count >= 1 && _allCars[0] != null) {
                int steering = 0;
                if (Input.GetKey(KeyCode.A)) steering = -1;
                else if (Input.GetKey(KeyCode.D)) steering = 1;

                _allCars[0].HandleInput(Input.GetKey(KeyCode.S), steering);
            }

            if (_allCars.Count >= 2 && _allCars[1] != null) {
                int steering = 0;
                if (Input.GetKey(KeyCode.LeftArrow)) steering = -1;
                else if (Input.GetKey(KeyCode.RightArrow)) steering = 1;

                _allCars[1].HandleInput(Input.GetKey(KeyCode.DownArrow), steering);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                _allCars[0].TakeDamage(15f);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                _allCars[0].Heal(10f);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3)) {
                _allCars[1].TakeDamage(15f);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4)) {
                _allCars[1].Heal(10f);
            }

            base.OnUpdate();
        }

        public void OnCarHealthChanged(int index, float health) {
            Debug.Log("Car " + index + " now has " + health + " health");
        }

        public void OnCarHealthDepleted(int index) {
            Debug.Log("Car " + index + " is dead!");
            RemoveChild(_allCars[index].View);
            RemoveChild(_allCars[index]);
            _allCars[index].Die();
            _allCars[index] = null;
        }
    }
}
