using Frictionless;
using System.Collections.Generic;

namespace LuftSchloss {
	public class InstanceBinder : IBindingStrategy {
        private static InstanceBinder _instance;
        private List<IBindingStrategy> _instanceBindings = new List<IBindingStrategy>();

        public static InstanceBinder Instance {
            get {
                if (_instance == null) {
                    _instance = new InstanceBinder();
                }

                return _instance;
            }
        }

        public void AddBinding(IBindingStrategy binding) {
            _instanceBindings.Add(binding);
        }

        public bool RemoveBinding(IBindingStrategy binding) {
            return _instanceBindings.Remove(binding);
        }

        public void BindInstances() {
            foreach (IBindingStrategy binding in _instanceBindings) {
                binding.BindInstances();
            }
        }

        public void ClearBindings() {
            var serviceFactory = ServiceFactory.Instance;
            var messageRouter = serviceFactory.Resolve<MessageRouter>();
            if (messageRouter != null) {
                messageRouter.Reset();
            }

            serviceFactory.Reset();
        }

        public static T Get<T>() where T : class {
            return ServiceFactory.Instance.Resolve<T>();
        }
    }
}
