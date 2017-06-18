using System;

namespace LuftSchloss.Pooling {
    public interface IPool<T> where T : IPoolable {
        void Initialize(Func<T> creationFunc, Action<T> destructor);
        T GetPooled();
    }
}
