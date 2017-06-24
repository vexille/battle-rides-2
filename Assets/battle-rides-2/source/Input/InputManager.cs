using Luderia.BattleRides2.Cars;
using Luderia.BattleRides2.Data;
using LuftSchloss.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Luderia.BattleRides2.InputHandling {
    public class InputManager : LuftMonobehaviour {
        [SerializeField]
        private InputMapping _mapping;
        private Dictionary<int, IInputHandler> _playerInputHandlers;
        private Dictionary<int, CarController> _playerControllers;
        private List<int> _activeCars;

        public override void Initialize() {
            base.Initialize();

            _playerInputHandlers = new Dictionary<int, IInputHandler>();
            _playerInputHandlers.Add(0, new KeyboardInputHandler(_mapping.P1KeyboardMapping));
            _playerInputHandlers.Add(1, new KeyboardInputHandler(_mapping.P2KeyboardMapping));

            _playerControllers = new Dictionary<int, CarController>();
            _activeCars = new List<int>();
        }

        public void AddPlayerController(CarController controller) {
            _playerControllers.Add(controller.CarIndex, controller);
            _activeCars.Add(controller.CarIndex);
        }

        public override void OnUpdate() {
            base.OnUpdate();

            foreach (var carIndex in _activeCars) {
                ParseCarInput(_playerControllers[carIndex], _playerInputHandlers[carIndex].ProcessInputs());
            }
        }

        private void ParseCarInput(CarController controller, List<InputAction> actions) {
            bool reversing = false;
            int steering = 0;
            int usedPowerup = -1;

            foreach (var action in actions) {
                if (action == InputAction.Reverse) {
                    reversing = true;
                    continue;
                }

                if (action == InputAction.SteerLeft) {
                    steering = -1;
                    continue;
                }

                if (action == InputAction.SteerRight) {
                    steering = 1;
                    continue;
                }

                if (action == InputAction.Powerup1) {
                    usedPowerup = 0;
                    continue;
                }

                if (action == InputAction.Powerup2) {
                    usedPowerup = 1;
                    continue;
                }

                if (action == InputAction.Powerup3) {
                    usedPowerup = 2;
                    continue;
                }

                if (action == InputAction.Powerup4) {
                    usedPowerup = 3;
                    continue;
                }
            }

            controller.HandleInput(reversing, steering, usedPowerup);
        }
    }
}
