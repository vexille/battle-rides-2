using Luderia.BattleRides2.Cars;
using Luderia.BattleRides2.Data;
using LuftSchloss;
using LuftSchloss.Util;
using System;
using System.Collections;
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
                CreateCarAt(currentPoint);

                GameObject.Destroy(currentPoint.gameObject);

                // oh god this is so ugly
                _allCars[_allCars.Count - 1].View.CarIndex = i;
            }
        }

        private void CreateCarAt(CarSpawnPoint spawnPoint) {
            var carController = AddChild<CarController>();
            carController.InitializeCar(_carShuffleBag.Next(), spawnPoint.transform.position, spawnPoint.transform.rotation);

            AddChild(carController.View);

            _allCars.Add(carController);
        }

        public override void OnUpdate() {
            if (_allCars.Count >= 1) {
                int steering = 0;
                if (Input.GetKey(KeyCode.A)) steering = -1;
                else if (Input.GetKey(KeyCode.D)) steering = 1;

                _allCars[0].HandleInput(Input.GetKey(KeyCode.S), steering);
            }

            if (_allCars.Count >= 2) {
                int steering = 0;
                if (Input.GetKey(KeyCode.LeftArrow)) steering = -1;
                else if (Input.GetKey(KeyCode.RightArrow)) steering = 1;

                _allCars[1].HandleInput(Input.GetKey(KeyCode.DownArrow), steering);
            }

            base.OnUpdate();
        }
    }
}
