using System.Collections.Generic;
using UnityEngine;

namespace LuftSchloss.Util {
    public class ShuffleBag<T> {
        private List<T> _data;
        private int _cursor;

        public int Count { get { return _data.Count; } }

        public ShuffleBag() {
            _cursor = -1;
            _data = new List<T>();
        }

        public void Add(T value) {
            _data.Add(value);
            _cursor = _data.Count - 1;
        }

        public void Add(T value, int count) {
            while (count-- > 0) {
                Add(value);
            }
        }

        public T Next() {
            if (_cursor < 1) {
                _cursor = _data.Count - 1;
                return _data[0];
            }

            int grab = Random.Range(0, _cursor + 1);
            T temp = _data[grab];

            _data.Swap(grab, _cursor);
            _cursor--;
            return temp;
        }

	    public void reset() {
            _data.Clear();
		    _cursor = -1;
	    }
    }
}
