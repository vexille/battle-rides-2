using System;

namespace LuftSchloss.Persistence {
	public interface IPersistence<T> {
        void AddChangeListener(Action<T> onChange);
        void RemoveChangeListener(Action<T> onChange);

        T GetDataCopy();
        void SetDataCopy(T data);

        T Load();
        void Save();
	}
}
