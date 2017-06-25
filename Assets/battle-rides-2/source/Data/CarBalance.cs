using UnityEngine;

namespace Luderia.BattleRides2.Data {
    [CreateAssetMenu(menuName = "BattleRides/Car balance data", fileName = "CarBalanceData")]
    public class CarBalance : ScriptableObject {
        public float MaxHealth = 100f;

        [SerializeField]
        private float _topSpeed = 20f;

        [SerializeField]
        private float _topSpeedReverse = 15f;

        [Tooltip("Value added to current speed per second")]
        [SerializeField]
        private float _acceleration = 10f;

        [Tooltip("Value subracted from current speed per second")]
        [SerializeField]
        private float _reverseAcceleration = 10f;

        public float MaxTurningAngle = 15f;

        private const float _inGameSpeedScale = 100f;

        public float TopSpeed { get { return _topSpeed; } }
        public float TopSpeedReverse { get { return _topSpeedReverse; } }
        public float Acceleration { get { return _acceleration * _inGameSpeedScale; } }
        public float ReverseAcceleration { get { return _reverseAcceleration * _inGameSpeedScale; } }

    }
}
