﻿using LuftSchloss.Events;
using UnityEngine;

namespace LuftSchloss.Core {
    public class LuftMonobehaviour : MonoBehaviour, ILuftObject {
        private bool _initialized;
        private EventDispatchList<ILuftObject> _children = new EventDispatchList<ILuftObject>();

        public virtual int Priority {
            get { return 0; }
        }

        public bool Initialized {
            get { return _initialized; }
            protected set { _initialized = value; }
        }

        public T AddChild<T>() where T : ILuftObject, new() {
            var child = new T();
            AddChild(child);
            return child; 
        }

        public void AddChild(ILuftObject child) {
            _children.AddListener(child);

            if (Initialized) {
                child.Initialize();
            }
        }

        public T FindChild<T>() where T : ILuftObject {
            return (T)_children.Listeners.Find(x => x is T);
        }

        public virtual void Initialize() {
            _children.Initialize();
            _initialized = true;
        }

        public virtual void OnStartState() {
            _children.OnStartState();
        }

        public virtual void OnDestroyState() {
            _children.OnDestroyState();
        }

        public virtual void OnUpdate() {
            _children.OnUpdate();
        }

        public virtual void OnFixedUpdate() {
            _children.OnFixedUpdate();
        }
    }
}