using Luderia.BattleRides2.Cars;
using Luderia.BattleRides2.Data;

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

    public class CarHealthChanged {
        public int CarIndex;
        public float Percentage;
    }

    public class PowerupSlotChanged {
        public int CarIndex;
        public int SlotIndex;
        public PowerupType Value;
    }
}
