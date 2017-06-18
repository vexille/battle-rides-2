using LuftSchloss.Core;
using UnityEngine;

namespace LuftSchloss {
	public abstract class AppState : LuftMonobehaviour {

        protected abstract IBindingStrategy CreateBinding();

        private void Awake() {
            Application.targetFrameRate = 60;

            InstanceBinder.Instance.AddBinding(CreateBinding());
            InstanceBinder.Instance.BindInstances();

            LuftMonobehaviour[] luftMonobehaviours = GameObject.FindObjectsOfType<LuftMonobehaviour>();
            for (int i = 0; i < luftMonobehaviours.Length; i++) {
                var luftMonobehaviour = luftMonobehaviours[i];
                if (luftMonobehaviour != this) {
                    AddChild(luftMonobehaviour);
                }
            }

            Initialize();
        }

        private void Start() {
            OnStartState();
        }

        private void OnDestroy() {
            OnDestroyState();
        }

        private void Update() {
            OnUpdate();
        }

        private void FixedUpdate() {
            OnFixedUpdate();
        }

	}
}
