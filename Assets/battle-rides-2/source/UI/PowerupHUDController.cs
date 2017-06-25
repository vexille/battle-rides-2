using LuftSchloss.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Luderia.BattleRides2.HUD {
    public class PowerupHUDController : LuftMonobehaviour {
        public Image[] PowerupSlots;

        public void UpdateSlot(int index, Sprite sprite) {
            PowerupSlots[index].sprite = sprite;
        }
    }
}
