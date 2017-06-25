using Frictionless;
using Luderia.BattleRides2.Data;
using LuftSchloss;
using UnityEngine;

namespace Luderia.BattleRides2.Powerups {
    public class PowerupDrop : MonoBehaviour {
        private PowerupType _powerupType;
        private SpriteRenderer _sprite;

        public int SpawnPointIndex;

        public bool Consumed { get; private set; }

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

        public void Consume() {
            Consumed = true;
            Destroy(gameObject);
        }

        private void OnDestroy() {
            var messageRouter = InstanceBinder.Get<MessageRouter>();
            if (messageRouter != null) {
                messageRouter.RaiseMessage(new OnPowerupPickedUp { SpawnPointIndex = SpawnPointIndex });
            }
        }
    }
}
