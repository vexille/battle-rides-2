using LuftSchloss.Core;
using UnityEngine;

namespace LuftSchloss {
	public abstract class AppState : LuftMonobehaviour {

        private bool _started;

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
            LateInitialize();
        }

        protected void StartState() {
            _started = true;
            OnStartState();
        }

        private void OnDestroy() {
            OnDestroyState();
        }

        private void Update() {
            if (_started) {
                OnUpdate();
            }
        }

        private void FixedUpdate() {
            if (_started) {
                OnFixedUpdate();
            }
        }

	}
}
