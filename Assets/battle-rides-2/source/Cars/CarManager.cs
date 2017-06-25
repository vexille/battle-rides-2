using Frictionless;
using Luderia.BattleRides2.Data;
using Luderia.BattleRides2.InputHandling;
using LuftSchloss;
using LuftSchloss.Core;
using LuftSchloss.Util;
using System.Collections.Generic;
using UnityEngine;

namespace Luderia.BattleRides2.Cars {
    public class CarManager : LuftMonobehaviour {
        [SerializeField]
        private CarDataList _carDataList;

        private List<CarController> _allCars;

        public override void Initialize() {
            base.Initialize();

            _allCars = new List<CarController>();
            ShuffleBag<CarData> carShuffleBag = new ShuffleBag<CarData>();
            for (int i = 0; i < _carDataList.CarList.Count; i++) {
                carShuffleBag.Add(_carDataList.CarList[i]);
            }

            CarSpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<CarSpawnPoint>();
            for (int i = 0; i < spawnPoints.Length; i++) {
                var currentPoint = spawnPoints[i];
                CreateCarAt(carShuffleBag, currentPoint, i);

                GameObject.Destroy(currentPoint.gameObject);
            }
        }

        public override void OnUpdate() {
            base.OnUpdate();

            //if (Input.GetKeyDown(KeyCode.Alpha1)) {
            //    _allCars[0].TakeDamage(15f);
            //}

            //if (Input.GetKeyDown(KeyCode.Alpha2)) {
            //    _allCars[0].Heal(10f);
            //}

            //if (Input.GetKeyDown(KeyCode.Alpha3)) {
            //    _allCars[1].TakeDamage(15f);
            //}

            //if (Input.GetKeyDown(KeyCode.Alpha4)) {
            //    _allCars[1].Heal(10f);
            //}
        }

        private void CreateCarAt(ShuffleBag<CarData> carShuffleBag, CarSpawnPoint spawnPoint, int index) {
            var carController = AddChild<CarController>();
            carController.InitializeCar(index, carShuffleBag.Next(), spawnPoint.transform.position, spawnPoint.transform.rotation);
            carController.OnHealthChanged += (i, health) => OnCarHealthChanged(i, health);
            carController.OnHealthDepleted += (i) => OnCarHealthDepleted(i);
            AddChild(carController.View);

            _allCars.Add(carController);
            InstanceBinder.Get<InputManager>().AddPlayerController(carController);
        }

        public void OnCarHealthChanged(int index, float health) {
            InstanceBinder.Get<MessageRouter>().RaiseMessage(new CarHealthChanged {
                CarIndex = index,
                Percentage = health
            });
        }

        public void OnCarHealthDepleted(int index) {
            Debug.Log("Car " + index + " is dead!");
            var carController = _allCars.Find(car => car.CarIndex == index);
            if (carController == null) {
                Debug.LogWarning("[CarManager] Car with index " + index + " supposedly died, but I can't find it");
                return;
            }

            RemoveChild(carController.View);
            RemoveChild(carController);
            carController.Die();
            _allCars.Remove(carController);
            InstanceBinder.Get<MessageRouter>().RaiseMessage(new CarDestroyed { CarIndex = index });
        }
    }
}
