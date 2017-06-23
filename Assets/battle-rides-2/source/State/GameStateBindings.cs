using LuftSchloss;
using UnityEngine;
using Frictionless;
using Luderia.BattleRides2.Util;

namespace Luderia.BattleRides2.States {
    public class GameStateBindings : IBindingStrategy {
        public void BindInstances() {
            var go = new GameObject("MonoProxy");
            ServiceFactory.Instance.RegisterSingleton<MonoProxy>(go.AddComponent<MonoProxy>());
            ServiceFactory.Instance.RegisterSingleton<MessageRouter>();
        }
    }
}
