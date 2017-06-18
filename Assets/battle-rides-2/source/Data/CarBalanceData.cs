using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BattleRides/Car balance data", fileName = "CarBalanceData")]
public class CarBalanceData : ScriptableObject {
    public float Speed = 20f;
    public float MaxTurningAngle = 15f;
}
