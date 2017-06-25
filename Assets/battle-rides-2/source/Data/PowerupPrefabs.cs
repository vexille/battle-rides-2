using UnityEngine;

namespace Luderia.BattleRides2.Data {
    [CreateAssetMenu(menuName = "BattleRides/Powerup Prefabs", fileName = "PowerupPrefabs")]
    public class PowerupPrefabs : ScriptableObject {
        public GameObject MissilePrefab;
        public GameObject BulletPrefab;
        public GameObject MinePrefab;
    }
}
