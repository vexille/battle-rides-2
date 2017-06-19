using UnityEngine;

namespace Luderia.BattleRides2.Cars {
    [System.Serializable]
    public class CarModel {
        public float CurrentSpeed;
        public float SteeringAngle;
        public Rigidbody Rigidbody;

        public float SteeringInput;
        public float AccelInput;
    }
}