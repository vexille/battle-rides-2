using System;

namespace LuftSchloss.Pooling {
    public interface IPoolable {
        float TimeMadeAvailable { get; }
        bool AvailableInPool { get; }
        void Activate();
        void Deactivate();
    }
}
