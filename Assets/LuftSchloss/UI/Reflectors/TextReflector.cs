using LuftSchloss.UI.Bindings;
using UnityEngine;
using UnityEngine.UI;

namespace LuftSchloss.UI.Reflectors {
    public class TextReflector : MonoBehaviour, IReflector {
        [SerializeField]
        private UIBinding _sourceBinding;

        [SerializeField]
        private Text _targetText;

        public IUIBinding SourceBinding {
            set { _sourceBinding = (UIBinding) value; }
        }

        private void Awake() {
            if (_targetText == null) {
                _targetText = GetComponent<Text>();
            }

            if (_sourceBinding == null) {
                _sourceBinding = GetComponent<UIBinding>();
            }

            if (_sourceBinding != null) {
                _sourceBinding.OnValueChanged += (o) => ReflectValue();
            } else {
                Debug.LogWarning("[TextReflector] No source binding.");
            }
        }

        public void ReflectValue() {
            _targetText.text = _sourceBinding.Value.ToString();
        }
    }
}
