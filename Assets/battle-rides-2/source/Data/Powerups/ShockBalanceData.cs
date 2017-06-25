using UnityEngine;

namespace Luderia.BattleRides2.Data {
    [CreateAssetMenu(menuName = "BattleRides/Powerups/Shock Balance Data", fileName = "ShockBalanceData")]
    public class ShockBalanceData : ScriptableObject {
        public float Damage = 16f;
        public float Duration = 3f;
    }
}
