using Luderia.BattleRides2.Data;
using LuftSchloss.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luderia.BattleRides2.Cars {
    public class MovementComponent : LuftController {
        private CarModel _model;
        private CarView _view;
        private CarBalance _carData;

        public MovementComponent(CarModel model, CarView view, CarBalance carData) {
            _model = model;
            _view = view;
            _carData = carData;
        }

        public override void OnUpdate() {
            base.OnUpdate();

            // Descomentar se por acaso vc quiser que todos os carros tenham o mesmo input
            //{
            //    _model.AccelInput = Input.GetKey(KeyCode.S) ? -1 : 1;

            //    if (Input.GetKey(KeyCode.A)) {
            //        _model.SteeringInput = -1;
            //    } else
            //    if (Input.GetKey(KeyCode.D)) {
            //        _model.SteeringInput = 1;
            //    } else {
            //        _model.SteeringInput = 0;
            //    }
            //}

            // Handle speed update
            if (_model.AccelInput > 0) {
                //_model.CurrentSpeed = Mathf.Min(
                //    _model.CurrentSpeed + _carData.Acceleration, 
                //    _carData.TopSpeed);

                _model.CurrentAccel = _carData.Acceleration;

            } else {
                //_model.CurrentSpeed = Mathf.Max(
                //    _model.CurrentSpeed - _carData.ReverseAcceleration, 
                //    -_carData.TopSpeedReverse);

                _model.CurrentAccel = -_carData.ReverseAcceleration;

            }

            if (_model.NitroOn) {
                _model.CurrentAccel *= _model.NitroBoost;
            }

            // Inverts angle when reversing
            if (_model.CurrentAccel < 0f) {
                _model.SteeringInput *= _model.AccelInput;
            }

            //_model.CurrentSpeed = _carData.TopSpeed * 100f * Input.GetAxis("Vertical");
            _model.SteeringAngle = _carData.MaxTurningAngle * _model.SteeringInput;
        }

        public override void OnFixedUpdate() {
            base.OnFixedUpdate();

            float currentSpeed = GetForwardVelocity().magnitude;
            bool shouldAddForce = false;
            if (_model.CurrentAccel > 0f && currentSpeed < _carData.TopSpeed) shouldAddForce = true;
            if (_model.CurrentAccel < 0f && currentSpeed < _carData.TopSpeedReverse) shouldAddForce = true;

            if (shouldAddForce) {
                var steeringDirection = _view.Steering.forward;
                var leftForce = steeringDirection * _model.CurrentAccel;
                var rightForce = steeringDirection * _model.CurrentAccel;

                _view.FrontLeftWheel.AddForce(leftForce);
                _view.FrontRightWheel.AddForce(rightForce);

                _view.DrawDebugLines(leftForce, rightForce);
            }

            UpdateFriction();
        }

        private void UpdateFriction() {
            Vector3 impulse = _model.Rigidbody.mass * -GetLateralVelocity();
            _model.Rigidbody.AddForce(impulse, ForceMode.Impulse);

            //_model.Rigidbody.AddTorque(0.1f * _model.Rigidbody.inertiaTensor.magnitude * -_model.Rigidbody.angularVelocity, ForceMode.Impulse);
            Vector3 currentForward = GetForwardVelocity();
            float currentForwardSpeed = currentForward.magnitude;
            float dragForceMagnitude = -2 * currentForwardSpeed;
            _model.Rigidbody.AddForce(dragForceMagnitude * currentForward, ForceMode.Force);
        }

        private Vector3 GetLateralVelocity() {
            var rightNormal = _view.transform.right;
            return Vector3.Dot(rightNormal, _model.Rigidbody.velocity) * rightNormal;
        }

        private Vector3 GetForwardVelocity() {
            var forwardNormal = _view.transform.forward;
            return Vector3.Dot(forwardNormal, _model.Rigidbody.velocity) * forwardNormal;
        }

        public float GetLinearSpeed() {
            return Vector3.Dot(_model.Rigidbody.velocity, _view.transform.forward);
        }
    }
}