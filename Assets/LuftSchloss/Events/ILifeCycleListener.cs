namespace LuftSchloss.Events {
	public interface ILifeCycleListener {
        void Initialize();
        void OnStartState();
        void OnDestroyState();
	}
}
