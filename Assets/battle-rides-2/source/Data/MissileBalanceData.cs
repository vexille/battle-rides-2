using UnityEngine;

namespace Luderia.BattleRides2.Data {
    [CreateAssetMenu(menuName = "BattleRides/Missile Balance Data", fileName = "MissileBalanceData")]
    public class MissileBalanceData : ScriptableObject {
        public float Damage = 10f;
        public float Speed = 10f;
    }
}
