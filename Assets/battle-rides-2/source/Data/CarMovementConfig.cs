using UnityEngine;

namespace Luderia.BattleRides2.Data {
    [CreateAssetMenu(menuName = "BattleRides/Car Movement Config", fileName = "CarMovementConfig")]
    public class CarMovementConfig : ScriptableObject {
        [Tooltip("Factor by which the acceleration is multiplied when the current velocity " +
        "has a diferent sign than the target speed. (e.g. current = 10, desired = -15)")]
        public float DirectionChangeFactor = 2f;

        [Tooltip("Factor by which the acceleration is multiplied when the car is turning")]
        public float TurningSpeedFactor = 0.8f;

        public float MaxLateralImpulse = 3f;
    }
}