namespace LuftSchloss.Events {
	public interface ILifeCycleListener {
        void Initialize();
        void LateInitialize();
        void OnStartState();
        void OnDestroyState();
	}
}
