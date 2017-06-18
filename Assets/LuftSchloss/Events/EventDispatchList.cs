using System.Collections.Generic;

namespace LuftSchloss.Events {
	public class EventDispatchList<T> : IGameEventListener
        where T : IGameEventListener {

        private readonly List<T> _elements = new List<T>();

        public List<T> Listeners {
            get { return _elements; }
        }

        public void AddListener(T listener) {
            _elements.Add(listener);
        }

        public void RemoveListener(T listener) {
            _elements.Remove(listener);
        }

        #region Events
        public void Initialize() {
            for (int i = 0; i < _elements.Count; i++) {
                _elements[i].Initialize();
            }
        }

        public void OnStartState() {
            for (int i = 0; i < _elements.Count; i++) {
                _elements[i].OnStartState();
            }
        }

        public void OnDestroyState() {
            for (int i = 0; i < _elements.Count; i++) {
                // TODO: Add test for this
                if (_elements[i] != null) {
                    _elements[i].OnDestroyState();
                }
            }
        }

        public void OnUpdate() {
            for (int i = 0; i < _elements.Count; i++) {
                _elements[i].OnUpdate();
            }
        }

        public void OnFixedUpdate() {
            for (int i = 0; i < _elements.Count; i++) {
                _elements[i].OnFixedUpdate();
            }
        }

        #endregion
    }
}
