using UnityEngine;

namespace Luderia.BattleRides2.Data {
    [CreateAssetMenu(menuName = "BattleRides/Car balance data", fileName = "CarBalanceData")]
    public class CarBalanceData : ScriptableObject {
        public float TopSpeed = 20f;
        public float TopSpeedReverse = 15f;

        [Tooltip("Value added to current speed per second")]
        public float Acceleration = 10f;

        [Tooltip("Value subracted from current speed per second")]
        public float ReverseAcceleration = 10f;
        public float MaxTurningAngle = 15f;
    }
}
