using System.Collections.Generic;
using System;

namespace LuftSchloss.Pooling {
    public class ObjectPool<T> : IPool<T> where T : class, IPoolable {
        private List<T> _elements;
        private Func<T> _factoryMethod;
        private Action<T> _destructor;

        public List<T> Elements {
            get { return _elements; }
        }

        public void Initialize(Func<T> factoryMethod, Action<T> destructor) {
            _elements = new List<T>();
            _factoryMethod = factoryMethod;
            _destructor = destructor;
        }

        public T GetPooled() {
            T returnObject = null;

            for (int i = 0; i < _elements.Count; i++) {
                if (_elements[i].AvailableInPool) {
                    returnObject = _elements[i];
                    break;
                }
            }

            if (returnObject == null) {
                T newItem = _factoryMethod();
                _elements.Add(newItem);
                returnObject = newItem;
            }

            return returnObject;
        }

        public void Destroy(T element) {
            _elements.Remove(element);
            _destructor(element);
        }

        public void Destroy(IList<T> elements) {
            for (int i = 0; i < elements.Count; i++) {
                Destroy(elements[i]);
            }
        }
    }
}
