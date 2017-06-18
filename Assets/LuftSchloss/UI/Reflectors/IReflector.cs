using LuftSchloss.UI.Bindings;

namespace LuftSchloss.UI.Reflectors {
    public interface IReflector {
        IUIBinding SourceBinding { set; }

        void ReflectValue();
    }
}
