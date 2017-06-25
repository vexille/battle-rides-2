using UnityEngine;

namespace Luderia.BattleRides2.Cars {
    [System.Serializable]
    public class CarModel {
        public float CurrentHealth;

        public float CurrentAccel;
        public float SteeringAngle;
        public Rigidbody Rigidbody;

        public float SteeringInput;
        public float AccelInput;

        public bool ShockOn;
        public bool NitroOn;
        public float NitroBoost;
    }
}