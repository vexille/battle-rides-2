using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luderia.BattleRides2.InputHandling {
    public interface IInputHandler {
        List<InputAction> ProcessInputs();
    }
}
