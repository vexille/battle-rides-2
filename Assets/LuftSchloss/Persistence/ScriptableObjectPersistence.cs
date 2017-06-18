using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LuftSchloss.Persistence {
    public class ScriptableObjectPersistence<T> : IPersistence<T> where T : ScriptableObject {
        private T _data;
        private string _typeName;
        private bool _loaded;
        private event Action<T> _onChangeEvent;

        public ScriptableObjectPersistence() {
            _typeName = typeof(T).Name;
        }

        public T GetDataCopy() {
            if (!_loaded) {
                throw new InvalidOperationException("[ScriptableObjectPersistence] Trying to access unloaded data");
            }

            return ScriptableObject.Instantiate(_data);
        }

        public void SetDataCopy(T data) {
            _data = ScriptableObject.Instantiate(data);

            if (_data != null) {
                _loaded = true;
            }

            DispatchOnChangeEvent();
        }

        public T Load() {
            T loadedInstance = Resources.Load<T>(GetResourcePath());
            if (loadedInstance == null) {
                Debug.LogWarning("[ScriptableObjectPersistence] No saved file for " + _typeName + " in path " + GetResourcePath() + ", creating new instance");
                _data = ScriptableObject.CreateInstance<T>();
            } else {
                _data = ScriptableObject.Instantiate(loadedInstance);
            }

            if (_loaded) {
                DispatchOnChangeEvent();
            }

            _loaded = true;
            return _data;
        }

        public void Save() {
            if (!_loaded) {
                throw new InvalidOperationException("[ScriptableObjectPersistence] Trying to save unloaded data");                
            }

#if UNITY_EDITOR
            AssetDatabase.CreateAsset(_data, "Assets/dwarfortinho/Resources/" + GetResourcePath() + ".asset");
            AssetDatabase.Refresh();
#else 
            throw new InvalidOperationException("[ScriptableObjectPersistence] Cannot persist ScriptableObjects outside Unity Editor");
#endif
        }

        public void AddChangeListener(Action<T> onChange) {
            _onChangeEvent += onChange;
        }

        public void RemoveChangeListener(Action<T> onChange) {
            _onChangeEvent -= onChange;
        }

        private void DispatchOnChangeEvent() {
            if (_onChangeEvent != null) {
                _onChangeEvent(_data);
            }
        }

        private string GetResourcePath() {
            return "data/" + _typeName;
        }
    }
}
