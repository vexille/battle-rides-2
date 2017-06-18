using System;
using UnityEngine;

namespace LuftSchloss.UI.Bindings {
    public class UIBinding : MonoBehaviour, IUIBinding  {
        
        [SerializeField]
        private string _key;
        private object _value;

        public string Key { get { return _key; } }

        public object Value { get { return _value; } }

        public Action<object> OnValueChanged { get; set; }

        public void UpdateValue(object newValue) {
            _value = newValue;
            if (OnValueChanged != null) {
                OnValueChanged(newValue);
            }
        }
    }
}
