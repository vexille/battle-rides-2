using Frictionless;
using Luderia.BattleRides2.Data;
using LuftSchloss;
using LuftSchloss.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Luderia.BattleRides2.HUD {
    [System.Serializable]
    public class PowerupSprite {
        public PowerupType Type;
        public Sprite PSprite;
    }

    public class HUDController : LuftMonobehaviour {
        public PowerupHUDController[] PowerupHuds;
        public List<PowerupSprite> SpriteMappings;

        private Dictionary<PowerupType, Sprite> _mappings;

        public override void Initialize() {
            base.Initialize();

            _mappings = new Dictionary<PowerupType, Sprite>();
            for (int i = 0; i < SpriteMappings.Count; i++) {
                _mappings.Add(SpriteMappings[i].Type, SpriteMappings[i].PSprite);
            }

            InstanceBinder.Get<MessageRouter>().AddHandler<PowerupSlotChanged>(message => {
                PowerupHuds[message.CarIndex].UpdateSlot(message.SlotIndex, _mappings[message.Value]);
            });
        }
    }
}