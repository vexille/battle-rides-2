﻿using System.Collections.Generic;
using UnityEngine;

namespace Luderia.BattleRides2.Data {
    public enum PowerupType {
        None = 0,
        Missile,
        MachineGun,
        Shock,
        Landmine,
        Nitrous,
        Repair
    }

    [System.Serializable]
    public class PowerupWeight {
        public PowerupType Type;
        public int Weight = 1;
    }

    [CreateAssetMenu(menuName = "BattleRides/Powerup Balance Data", fileName = "PowerupBalanceData")]
    public class PowerupBalanceData : ScriptableObject {
        public static int MaxPowerupSlots = 4;

        public GameObject PowerupDropPrefab;
        public float PowerupSpawnInterval = 2f;
        public int MaxActivePowerupPickups = 2;

        public List<PowerupWeight> PowerupWeights;
    }

    [CreateAssetMenu(menuName = "BattleRides/Missile Balance Data", fileName = "MissileBalanceData")]
    public class MissileBalanceData : ScriptableObject {
        public float MissileDamage = 10f;
        public float MissileSpeed = 10f;
    }
}
