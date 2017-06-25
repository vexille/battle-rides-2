using UnityEngine;

namespace Luderia.BattleRides2.Data {
    [CreateAssetMenu(menuName = "BattleRides/Powerups/Nitro Balance Data", fileName = "NitroBalanceData")]
    public class NitroBalanceData : ScriptableObject {
        public float Boost = 2f;
        public float Duration = 1f;
    }
}
