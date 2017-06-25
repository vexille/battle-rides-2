using LuftSchloss.Events;

namespace LuftSchloss.Core {
    public interface ILuftObject : IGameEventListener {
        // TODO: Move to IGameEventListener
        int Priority { get; }

        bool Initialized { get; }

        T AddChild<T>() where T : ILuftObject, new();

        void AddChild(ILuftObject child);

        T FindChild<T>() where T : ILuftObject;

        void RemoveChild(ILuftObject child);
    }
}
