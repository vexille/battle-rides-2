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
        public PowerupComponent PowerupComp { get; private set; }

        public CarView View { get { return _view; } }

        public int CarIndex { get { return _index; } }


        public void InitializeCar(int index, CarData carData, Vector3 position, Quaternion rotation) {
            _index = index;
            _carData = carData.Data;
            _view = GameObject.Instantiate(carData.Prefab, position, rotation).GetComponent<CarView>();

            _model = new CarModel();
            _model.Rigidbody = _view.GetComponent<Rigidbody>();
            _model.CurrentHealth = _carData.MaxHealth;

            _view.Controller = this;
            _view.CarIndex = _index;
            _view.Model = _model;

            _movement = new MovementComponent(_model, _view, _carData);
            AddChild(_movement);
            PowerupComp = AddChild<PowerupComponent>();
            PowerupComp.Owner = this;
        }

        public void HandleInput(bool reversing, int steering, int usedPowerup) {
            _model.AccelInput = reversing ? -1 : 1;
            _model.SteeringInput = steering;
            if (usedPowerup != -1) {
                PowerupComp.UsePowerup(usedPowerup);
            }
        }

        public void TakeDamage(float damage, bool ignoreFeedback = false) {
            _model.CurrentHealth -= damage;
            if (_model.CurrentHealth <= 0f) {
                OnHealthDepleted.SafeCall(_index);
                _model.CurrentHealth = 0f;
            }

            OnHealthChanged.SafeCall(_index, _model.CurrentHealth / _carData.MaxHealth);

            if (!ignoreFeedback) {
                View.FireFeedbackTrigger(CarView.HitTrigger);
            }
        }

        public void Heal(float amount) {
            _model.CurrentHealth = Mathf.Min(_model.CurrentHealth + amount, _carData.MaxHealth);
            OnHealthChanged.SafeCall(_index, _model.CurrentHealth / _carData.MaxHealth);
        }

        public void SetNitro(bool value, float boost) {
            _model.NitroOn = value;
            _model.NitroBoost = boost;
        }

        public void SetNitro(bool value) {
            _model.NitroOn = value;
        }

        public void SetShock(bool value) {
            _model.ShockOn = value;
        }

        public void ShockHit(CarController target) {
            PowerupComp.HandleShockHit(target);
        }

        public void ShockTaken(CarController shocker) {
            PowerupComp.HandleShockTaken(shocker);
            View.FireFeedbackTrigger(CarView.ShockHitTrigger);
        }

        public void Die() {
            GameObject.Destroy(_view.gameObject);
        }
    }
}
