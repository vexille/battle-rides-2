using System;

namespace LuftSchloss.UI.Bindings {
    public interface IUIBinding {
        string Key { get; }

        object Value { get; }

        Action<object> OnValueChanged { get; set; }

        void UpdateValue(object newValue);
    }
}
