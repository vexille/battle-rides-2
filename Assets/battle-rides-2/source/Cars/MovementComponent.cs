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
        private CarMovementConfig _moveConfig;

        //private Rigidbody[] _tires;

        private bool IsReversing {
            get {
                return _model.AccelInput < 0f;
            }
        }

        private bool IsSteering {
            get {
                return _model.SteeringInput != 0;
            }
        }

        public MovementComponent(CarModel model, CarView view, CarBalance carData, CarMovementConfig movementConfig) {
            _model = model;
            _view = view;
            _carData = carData;
            _moveConfig = movementConfig;

            //_tires = new Rigidbody[] {
            //    view.FrontLeftWheel,
            //    view.FrontRightWheel,
            //    view.RearLeftWheel,
            //    view.RearRightWheel
            //};
        }

        public override void OnUpdate() {
            base.OnUpdate();

            // Handle speed update
            if (_model.AccelInput > 0) {
                _model.CurrentAccel = _carData.Acceleration;

            } else {
                _model.CurrentAccel = -_carData.ReverseAcceleration;
            }

            if (_model.NitroOn) {
                _model.CurrentAccel = Mathf.Abs(_model.CurrentAccel) * _model.NitroBoost;
            }

            //Comment to invert angle when reversing
            if (_model.CurrentAccel < 0f) {
                _model.SteeringInput *= _model.AccelInput;
            }

            //_model.CurrentSpeed = _carData.TopSpeed * 100f * Input.GetAxis("Vertical");
            _model.SteeringAngle = _carData.MaxTurningAngle * _model.SteeringInput;
        }

        public override void OnFixedUpdate() {
            base.OnFixedUpdate();

            UpdateDrive();
            UpdateFriction();
        }

        private void UpdateDrive() {
            float currentSpeed = GetForwardVelocity().magnitude;
            bool shouldAddForce = false;
            if (_model.CurrentAccel > 0f && currentSpeed < _carData.TopSpeed) shouldAddForce = true;
            if (_model.CurrentAccel < 0f && currentSpeed < _carData.TopSpeedReverse) shouldAddForce = true;

            if (!shouldAddForce) {
                return;
            }
            
            //float speedIncrement = _carData.TopSpeed * 0.2f;

            //float desiredSpeed = _model.AccelInput > 0 ?
            //    Mathf.Min(currentSpeed + speedIncrement, _carData.TopSpeed) :
            //    Mathf.Max(currentSpeed - speedIncrement, -_carData.TopSpeedReverse);

            var steeringDirection = _view.Steering.forward;
            var force = steeringDirection * _model.CurrentAccel;

            if (IsSteering) {
                force *= _moveConfig.TurningSpeedFactor;
            }

            if (IsReversing) {
                _view.RearLeftWheel.AddForce(force);
                _view.RearRightWheel.AddForce(force);
            } else {
                _view.FrontLeftWheel.AddForce(force);
                _view.FrontRightWheel.AddForce(force);
            }

            //_view.DrawDebugLines(leftForce, rightForce);
        }

        private void UpdateFriction() {
            Vector3 impulse = _model.Rigidbody.mass * -GetLateralVelocity();
            var lateralImpulse = impulse.magnitude;
            if (lateralImpulse > _moveConfig.MaxLateralImpulse) {
                impulse *= _moveConfig.MaxLateralImpulse / lateralImpulse;
            }

            _model.Rigidbody.AddForce(impulse, ForceMode.Impulse);

            //_model.Rigidbody.AddTorque(0.1f * _model.Rigidbody.inertiaTensor.magnitude * -_model.Rigidbody.angularVelocity, ForceMode.Impulse);
            Vector3 currentForward = GetForwardVelocity();
            float currentForwardSpeed = currentForward.magnitude;
            float dragForceMagnitude = -2f * currentForwardSpeed;
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