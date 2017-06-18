using LuftSchloss.Core;
using LuftSchloss.UI.Bindings;
using System.Collections.Generic;

namespace LuftSchloss.UI {
    public class UIRoot : LuftMonobehaviour {
        // Binding dictionary
        private Dictionary<string, List<UIBinding>> _bindingMap;

        public override int Priority {
            get {
                return (int) LuftPriority.High;
            }
        }

        public override void Initialize() {
            // Find all UI objects
            LuftUIObject[] uiObjects = GetComponentsInChildren<LuftUIObject>(true);
            for (int i = 0; i < uiObjects.Length; i++) {
                var uiObject = uiObjects[i];
                uiObject.InitializeUI(this);
                AddChild(uiObject);
            }
            
            _bindingMap = new Dictionary<string, List<UIBinding>>();

            // Find all bindings
            UIBinding[] bindings = gameObject.GetComponentsInChildren<UIBinding>(true);
            for (int i = 0; i < bindings.Length; i++) {
                AddBinding(bindings[i]);
            }

            base.Initialize();
        }

        private void AddBinding(UIBinding binding) {
            string key = binding.Key;

            List<UIBinding> targetList;
            if (!_bindingMap.TryGetValue(key, out targetList)) {
                targetList = new List<UIBinding>();
                _bindingMap.Add(key, targetList);
            }

            if (!targetList.Contains(binding)) {
                targetList.Add(binding);
            }
        }
        // methods to update bindings
    }
}
