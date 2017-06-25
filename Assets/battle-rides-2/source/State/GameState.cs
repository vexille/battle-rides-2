using LuftSchloss;
using System.Collections;
using UnityEngine;

namespace Luderia.BattleRides2.States {
    public class GameState : AppState {
        public bool CutToTheChase = false;
        protected override IBindingStrategy CreateBinding() {
            return new GameStateBindings();
        }

        public override void Initialize() {
            base.Initialize();

            StartCoroutine(StartCountdown());
        }

        private IEnumerator StartCountdown() {
            if (!CutToTheChase) {
                InstanceBinder.Get<SoundController>().PlaySound("match-start");
                Debug.Log("Start your engines!");
                Debug.Log("3...");
                yield return new WaitForSeconds(0.4f);
                Debug.Log("2...");
                yield return new WaitForSeconds(1f);
                Debug.Log("1...");
                yield return new WaitForSeconds(1f);
            }

            StartState();
            Debug.Log("Go!");
        }
    }
}
