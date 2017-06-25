using UnityEngine;

namespace Luderia.BattleRides2.Data {
    [CreateAssetMenu(menuName = "BattleRides/Powerups/Missile Balance Data", fileName = "MissileBalanceData")]
    public class MissileBalanceData : ScriptableObject {
        public float Damage = 14f;
        public float Speed = 40f;
    }
}
