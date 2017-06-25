using Luderia.BattleRides2.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luderia.BattleRides2.InputHandling {

    public class KeyboardInputHandler : IInputHandler {
        private Dictionary<InputAction, KeyCode> _keyMappings;

        public KeyboardInputHandler(List<KeyMapping> rawMappings) {
            _keyMappings = new Dictionary<InputAction, KeyCode>();

            for (int i = 0; i < rawMappings.Count; i++) {
                _keyMappings.Add(rawMappings[i].Action, rawMappings[i].Key);
            }
        }

        // TODO: come on, you can do better than this
        public List<InputAction> ProcessInputs() {
            var actions = new List<InputAction>();

            if (Input.GetKey(_keyMappings[InputAction.SteerLeft])) {
                actions.Add(InputAction.SteerLeft);
            } else if (Input.GetKey(_keyMappings[InputAction.SteerRight])) {
                actions.Add(InputAction.SteerRight);
            }

            if (Input.GetKey(_keyMappings[InputAction.Reverse])) {
                actions.Add(InputAction.Reverse);
            }

            if (Input.GetKeyDown(_keyMappings[InputAction.Powerup1])) {
                actions.Add(InputAction.Powerup1);
            }

            if (Input.GetKeyDown(_keyMappings[InputAction.Powerup2])) {
                actions.Add(InputAction.Powerup2);
            }

            if (Input.GetKeyDown(_keyMappings[InputAction.Powerup3])) {
                actions.Add(InputAction.Powerup3);
            }

            if (Input.GetKeyDown(_keyMappings[InputAction.Powerup4])) {
                actions.Add(InputAction.Powerup4);
            }

            return actions;
        }
    }
}
