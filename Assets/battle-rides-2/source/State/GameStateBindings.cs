using LuftSchloss;
using UnityEngine;
using Frictionless;
using Luderia.BattleRides2.Util;
using Luderia.BattleRides2.InputHandling;
using Luderia.BattleRides2.Powerups;

namespace Luderia.BattleRides2.States {
    public class GameStateBindings : IBindingStrategy {
        public void BindInstances() {
            var go = new GameObject("MonoProxy");
            ServiceFactory.Instance.RegisterSingleton<MonoProxy>(go.AddComponent<MonoProxy>());
            ServiceFactory.Instance.RegisterSingleton<MessageRouter>();
            ServiceFactory.Instance.RegisterSingleton<InputManager>(GameObject.FindObjectOfType<InputManager>());
            ServiceFactory.Instance.RegisterSingleton<PowerupManager>(GameObject.FindObjectOfType<PowerupManager>());
            ServiceFactory.Instance.RegisterSingleton<SoundController>(GameObject.FindObjectOfType<SoundController>());
        }
    }
}
