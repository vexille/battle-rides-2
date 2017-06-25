using Frictionless;
using Luderia.BattleRides2.Data;
using LuftSchloss;
using UnityEngine;

namespace Luderia.BattleRides2.Powerups {
    public class PowerupDrop : MonoBehaviour {
        private PowerupType _powerupType;
        private SpriteRenderer _sprite;
		public Sprite Missil;
		public Sprite MachineGun;
		public Sprite Shock;
		public Sprite Landmine;
		public Sprite Nitrous;
		public Sprite Repair;

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
					_sprite.sprite = Missil;
                    break;
			case PowerupType.MachineGun:
				_sprite.sprite = MachineGun;
                    break;
			case PowerupType.Shock:
				_sprite.sprite = Shock;
                    break;
			case PowerupType.Landmine:
				_sprite.sprite = Landmine;
                    break;
			case PowerupType.Nitrous:
				_sprite.sprite = Nitrous;
                    break;
			case PowerupType.Repair:
				_sprite.sprite = Repair;
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
