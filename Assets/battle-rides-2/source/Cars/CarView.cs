using Luderia.BattleRides2.Data;
using Luderia.BattleRides2.Powerups;
using LuftSchloss.Core;
using UnityEngine;

namespace Luderia.BattleRides2.Cars {
    public class CarView : LuftMonobehaviour {
        public const string MissileTrigger      = "Missil";
        public const string MachineGunTrigger   = "Metralhadora";
        public const string ShockTrigger        = "Choque";
        public const string ShockHitTrigger     = "Generic_Hit";
        public const string MineTrigger         = "Mina";
        public const string RepairTrigger       = "Cura";
        public const string NitroTrigger        = "Nitro";
        public const string HitTrigger          = "Generic_Hit";

        public Transform Steering;
        public Rigidbody FrontLeftWheel;
        public Rigidbody FrontRightWheel;
        public Rigidbody RearLeftWheel;
        public Rigidbody RearRightWheel;
        public Transform MissilePivot;
        public bool DebugText = false;

        public CarModel Model;
        private Rigidbody _rigidbody;
        private Animator _feedbackAnimator;

        public int CarIndex { get; set; }

        public CarController Controller { get; set; }

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
            _feedbackAnimator = GetComponent<Animator>();
        }

        public override void OnUpdate() {
 	        base.OnUpdate();

            Steering.localRotation = Quaternion.Euler(0f, Model.SteeringAngle, 0f);
        }

        public void DrawDebugLines(Vector3 leftForce, Vector3 rightForce) {
            if (DebugText) {
                Debug.DrawLine(FrontLeftWheel.transform.position, FrontLeftWheel.transform.position + FrontLeftWheel.transform.forward, Color.blue);
                Debug.DrawLine(FrontRightWheel.transform.position, FrontRightWheel.transform.position + FrontRightWheel.transform.forward, Color.blue);

                Debug.DrawLine(FrontLeftWheel.transform.position, FrontLeftWheel.transform.position + leftForce.normalized);
                Debug.DrawLine(FrontRightWheel.transform.position, FrontRightWheel.transform.position + rightForce.normalized);
            }
        }

        private void OnGUI() {
            if (DebugText) {
                var mainVelocity = _rigidbody.velocity;

                GUIStyle style = new GUIStyle();
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = Color.yellow;
                for (int i = 0; i < CarIndex; i++) {
                    GUILayout.Space(15f);
                }
                GUILayout.Label("Car " + CarIndex + string.Format(" speed: {0:0.000} ({1:0.#}, {2:0.#}, {3:0.#})", mainVelocity.magnitude, mainVelocity.x, mainVelocity.y, mainVelocity.z), style);
            }
        }

        public void FireFeedbackTrigger(string trigger) {
            _feedbackAnimator.SetTrigger(trigger);
        }

        public void SetFeedbackBool(string trigger, bool value) {
            _feedbackAnimator.SetBool(trigger, value);
        }

        private void OnCollisionEnter(Collision collision) {
            var damageInflictor = collision.gameObject.GetComponent<DamageInflictor>();
            if (damageInflictor != null) {
                Controller.TakeDamage(damageInflictor.Damage);
            }

            if (Model.ShockOn && collision.rigidbody != null) {
                var otherCar = collision.rigidbody.GetComponent<CarView>();
                if (otherCar != null && !otherCar.Model.ShockOn) {
                    Controller.ShockHit(otherCar.Controller);
                    otherCar.Controller.ShockTaken(Controller);
                }
            }
        }

        private void OnTriggerEnter(Collider other) {
            var damageInflictor = other.GetComponent<DamageInflictor>();
            if (damageInflictor != null) {
                Controller.TakeDamage(damageInflictor.Damage);
                return;
            }

            var powerupDrop = other.GetComponent<PowerupDrop>();
            if (powerupDrop != null && !powerupDrop.Consumed) {
                if (Controller.PowerupComp.AddPowerup(powerupDrop.Type)) {
                    powerupDrop.Consume();
                    return;
                }
            }
        }
    }
}