using Luderia.BattleRides2.Cars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luderia.BattleRides2 {
    public class OnPowerupPickedUp {
        public int SpawnPointIndex;
    }

    public class CarCreated {
        public CarController Car;
    }

    public class CarDestroyed {
        public int CarIndex;
    }
}
