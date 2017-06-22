using UnityEngine;

namespace Luderia.BattleRides2.Data {
    [CreateAssetMenu(menuName="BattleRides/Game Balance Data", fileName="GameBalanceData")]
    public class GameBalanceData : ScriptableObject {
        public CarDataList CarList;
        public PowerupBalanceData PowerupData;
    }
}