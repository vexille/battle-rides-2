using Frictionless;
using Luderia.BattleRides2.Data;
using LuftSchloss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luderia.BattleRides2.Powerups {
    public class PowerupDrop : MonoBehaviour {
        private PowerupType _powerupType;
        private SpriteRenderer _sprite;

        public PowerupType Type { 
            get { return _powerupType; }
            set {
                _powerupType = value;
                UpdateSprite();
            }
        }

        private void Awake() {
            _sprite = GetComponentInChildren<SpriteRenderer>();
        }

        private void UpdateSprite() {
            switch (_powerupType) {
                case PowerupType.Missile:
                    _sprite.color = Color.red;
                    break;
                case PowerupType.MachineGun:
                    _sprite.color = Color.yellow;
                    break;
                case PowerupType.Shock:
                    _sprite.color = Color.blue;
                    break;
                case PowerupType.Landmine:
                    _sprite.color = Color.black;
                    break;
                case PowerupType.Nitrous:
                    _sprite.color = Color.cyan;
                    break;
                case PowerupType.Repair:
                    _sprite.color = Color.green;
                    break;
                default:
                    break;
            }
        }

        private void OnDestroy() {
            var messageRouter = InstanceBinder.Get<MessageRouter>();
            if (messageRouter != null) {
                messageRouter.RaiseMessage(new OnPowerupPickedUp());
            }
        }
    }
}
