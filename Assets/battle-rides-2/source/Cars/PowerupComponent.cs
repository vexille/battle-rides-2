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
                    UnityEngine.Debug.Log("Add powerup: " + type + " in slot " + i);
                    return true;
                }
            }

            return false;
        }

        public void UsePowerup(int slotIndex) {
            UnityEngine.Debug.Log("Use slot: " + slotIndex + " > " + _powerupSlots[slotIndex]);
            _powerupSlots[slotIndex] = PowerupType.None;
        }
    }
}