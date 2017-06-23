using Luderia.BattleRides2.Data;
using LuftSchloss.Core;

namespace Luderia.BattleRides2.Cars {
    public class PowerupComponent : LuftController {
        private PowerupType[] _powerupSlots;

        public override void Initialize() {
            base.Initialize();

            _powerupSlots = new PowerupType[PowerupBalanceData.MaxPowerupSlots];
        }

        public bool AddPowerup(PowerupType type) {
            for (int i = 0; i < _powerupSlots.Length; i++) {
                if (_powerupSlots[i] == PowerupType.None) {
                    _powerupSlots[i] = type;
                    return true;
                }
            }

            return false;
        }
    }
}