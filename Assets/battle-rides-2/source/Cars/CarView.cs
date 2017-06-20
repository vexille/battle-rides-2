using Luderia.BattleRides2.Data;
using LuftSchloss.Core;
using UnityEngine;

namespace Luderia.BattleRides2.Cars {
    public class CarView : LuftMonobehaviour {
        public Transform Steering;
        public Rigidbody FrontLeftWheel;
        public Rigidbody FrontRightWheel;
        public bool DebugText = false;

        public CarModel Model;
        private Rigidbody _rigidbody;
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public override void OnUpdate() {
 	        base.OnUpdate();

            Steering.localRotation = Quaternion.Euler(0f, Model.SteeringAngle, 0f);
        }

        public void DrawDebugLines(Vector3 leftForce, Vector3 rightForce) {
            Debug.DrawLine(FrontLeftWheel.transform.position, FrontLeftWheel.transform.position + FrontLeftWheel.transform.forward, Color.blue);
            Debug.DrawLine(FrontRightWheel.transform.position, FrontRightWheel.transform.position + FrontRightWheel.transform.forward, Color.blue);

            Debug.DrawLine(FrontLeftWheel.transform.position, FrontLeftWheel.transform.position + leftForce.normalized);
            Debug.DrawLine(FrontRightWheel.transform.position, FrontRightWheel.transform.position + rightForce.normalized);
        }

        private void OnGUI() {
            if (DebugText) {
                var mainVelocity = _rigidbody.velocity;

                GUIStyle style = new GUIStyle();
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = Color.yellow;

                GUILayout.Label(string.Format("Main speed: {0:0.000} ({1:0.#}, {2:0.#}, {3:0.#})", mainVelocity.magnitude, mainVelocity.x, mainVelocity.y, mainVelocity.z), style);
            }
        }
    }
}