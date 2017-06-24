using Luderia.BattleRides2.Data;
using LuftSchloss;
using LuftSchloss.Core;
using System;
using UnityEngine;

namespace Luderia.BattleRides2.Cars {
    public class CarController : LuftController {
        public event Action<int> OnHealthDepleted;
        public event Action<int, float> OnHealthChanged;

        private int _index;

        private CarView _view;
        private CarModel _model;
        private CarBalance _carData;

        private MovementComponent _movement;

        public CarView View { get { return _view; } }

        public int CarIndex { get { return _index; } }

        public PowerupComponent PowerupComp { get; private set; }

        public void InitializeCar(int index, CarData carData, Vector3 position, Quaternion rotation) {
            _index = index;
            _carData = carData.Data;
            _view = GameObject.Instantiate(carData.Prefab, position, rotation).GetComponent<CarView>();

            _model = new CarModel();
            _model.Rigidbody = _view.GetComponent<Rigidbody>();
            _model.CurrentHealth = _carData.MaxHealth;

            _view.Controller = this;
            _view.Model = _model;
            _view.CarIndex = _index;

            _movement = new MovementComponent(_model, _view, _carData);
            AddChild(_movement);
            PowerupComp = AddChild<PowerupComponent>();
        }

        public void HandleInput(bool reversing, int steering, int usedPowerup) {
            _model.AccelInput = reversing ? -1 : 1;
            _model.SteeringInput = steering;
            if (usedPowerup != -1) {
                PowerupComp.UsePowerup(usedPowerup);
            }
        }

        public void TakeDamage(float damage) {
            _model.CurrentHealth -= damage;
            if (_model.CurrentHealth <= 0f) {
                OnHealthDepleted.SafeCall(_index);
                _model.CurrentHealth = 0f;
            }

            OnHealthChanged.SafeCall(_index, _model.CurrentHealth);
        }

        public void Heal(float amount) {
            _model.CurrentHealth = Mathf.Min(_model.CurrentHealth + amount, _carData.MaxHealth);
            OnHealthChanged.SafeCall(_index, _model.CurrentHealth);
        }

        public void Die() {
            GameObject.Destroy(_view.gameObject);
        }
    }
}
