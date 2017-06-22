using System.Collections.Generic;
using UnityEngine;

namespace Luderia.BattleRides2.Data {
    public enum PowerupType {
        Missile,
    }

    [System.Serializable]
    public class PowerupWeight {
        public PowerupType Type;
        public int Weight = 1;
    }

    [CreateAssetMenu(menuName = "BattleRides/Powerup Balance Data", fileName = "PowerupBalanceData")]
    public class PowerupBalanceData : ScriptableObject {
        public float PowerupSpawnInterval = 2f;
        public int MaxActivePowerupPickups = 2;

        public List<PowerupWeight> PowerupWeights;

        [Header("Missile")]
        public float MissileDamage = 10f;
        public float MissileSpeed = 10f;
    }
}
