using Luderia.BattleRides2.Data;
using LuftSchloss.Core;
using UnityEngine;

namespace Luderia.BattleRides2.Cars {
    public class CarController : LuftController {
        private CarBalanceData _carData;
        private CarView _view;
        private CarModel _model;

        public CarView View { get { return _view; } }

        public void InitializeCar(CarPrefabDataPair carData, Vector3 position, Quaternion rotation) {
            _carData = carData.Data;
            _view = GameObject.Instantiate(carData.Prefab, position, rotation).GetComponent<CarView>();

            _model = new CarModel();
            _model.Rigidbody = _view.GetComponent<Rigidbody>();

            _view.Model = _model;
        }

        public override void OnUpdate() {
            base.OnUpdate();

            { // TODO: Tacar isso no controle de input
                _model.AccelInput = Input.GetKey(KeyCode.S) ? -1 : 1;

                if (Input.GetKey(KeyCode.A)) {
                    _model.SteeringInput = -1;
                } else
                if (Input.GetKey(KeyCode.D)) {
                    _model.SteeringInput = 1;
                } else {
                    _model.SteeringInput = 0;
                }
            }

            // Handle speed update
            if (_model.AccelInput > 0) {
                _model.CurrentSpeed = Mathf.Min(_model.CurrentSpeed + _carData.Acceleration, _carData.TopSpeed * 100f);
            } else {
                _model.CurrentSpeed = Mathf.Max(_model.CurrentSpeed - _carData.ReverseAcceleration, -_carData.TopSpeedReverse * 100f);
            }
            
            // Inverts angle when reversing
            _model.SteeringInput *= _model.AccelInput;

            //_model.CurrentSpeed = _carData.TopSpeed * 100f * Input.GetAxis("Vertical");
            _model.SteeringAngle = _carData.MaxTurningAngle * _model.SteeringInput;
        }

        public override void OnFixedUpdate() {
            base.OnFixedUpdate();

            var steeringDirection = _view.Steering.forward;
            var leftForce = steeringDirection * _model.CurrentSpeed;
            var rightForce = steeringDirection * _model.CurrentSpeed;

            _view.FrontLeftWheel.AddForce(leftForce);
            _view.FrontRightWheel.AddForce(rightForce);

            _view.DrawDebugLines(leftForce, rightForce);
            UpdateFriction();
        }

        private void UpdateFriction() {
            Vector3 impulse = _model.Rigidbody.mass * -GetLateralVelocity();
            _model.Rigidbody.AddForce(impulse, ForceMode.Impulse);
        }

        private Vector3 GetLateralVelocity() {
            var rightNormal = _view.transform.right;
            return Vector3.Dot(rightNormal, _model.Rigidbody.velocity) * rightNormal;
        }

        public float GetLinearSpeed() {
            return Vector3.Dot(_model.Rigidbody.velocity, _view.transform.forward);
        }
    }
}
