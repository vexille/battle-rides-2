using LuftSchloss.Events;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

public class EventDispatchListTest {
    private const int LISTENER_COUNT = 128;

    [Test]
    public void AddListener() {
        var dispatcherList = GetDispatchList();
        var dummy = new DummyListener();
        dispatcherList.AddListener(dummy);
        Assert.AreEqual(1, dispatcherList.Listeners.Count);
    }

    [Test]
    public void RemoveListener() {
        var dispatcherList = GetDispatchList();
        var dummy = new DummyListener();
        dispatcherList.AddListener(dummy);
        dispatcherList.RemoveListener(dummy);
        Assert.AreEqual(0, dispatcherList.Listeners.Count);
    }

	[Test]
    public void Initialize() {
        var dispatcherList = GetDispatchList();
        var listenerList = InitializeListeners(dispatcherList); 
        dispatcherList.Initialize();
        AssertHits(listenerList, hitCounts => hitCounts.Initialize );
    }

    [Test]
    public void OnStartState() {
        var dispatcherList = GetDispatchList();
        var listenerList = InitializeListeners(dispatcherList);
        dispatcherList.OnStartState();
        AssertHits(listenerList, hitCounts => hitCounts.OnStartState);
    }

    [Test]
    public void OnDestroyState() {
        var dispatcherList = GetDispatchList();
        var listenerList = InitializeListeners(dispatcherList);
        dispatcherList.OnDestroyState();
        AssertHits(listenerList, hitCounts => hitCounts.OnDestroyState);
    }

    [Test]
    public void Update() {
        var dispatcherList = GetDispatchList();
        var listenerList = InitializeListeners(dispatcherList);
        dispatcherList.OnUpdate();
        AssertHits(listenerList, hitCounts => hitCounts.Update);
    }

    [Test]
    public void FixedUpdate() {
        var dispatcherList = GetDispatchList();
        var listenerList = InitializeListeners(dispatcherList);
        dispatcherList.OnFixedUpdate();
        AssertHits(listenerList, hitCounts => hitCounts.FixedUpdate);
    }

    private List<DummyListener> InitializeListeners(EventDispatchList<DummyListener> dispatcherList) {
        var listenerList = new List<DummyListener>();
        Enumerable.Range(0, LISTENER_COUNT).ToList().ForEach((i) => listenerList.Add(new DummyListener()));
        listenerList.ForEach(listener => dispatcherList.AddListener(listener));
        return listenerList;
    }

    private void AssertHits(List<DummyListener> listenerList, Func<EventHitCounts, int> hitCountAccessFunc) {
        var count = 0;
        listenerList.ForEach(listener => {
            count += hitCountAccessFunc(listener.HitCounts);
        });
        
        Assert.AreEqual(LISTENER_COUNT, count);
    }

    private EventDispatchList<DummyListener> GetDispatchList() {
        return new EventDispatchList<DummyListener>();
    }

    public class EventHitCounts {
        public int Initialize;
        public int OnStartState;
        public int OnDestroyState;
        public int Update;
        public int FixedUpdate;
    }

    public class DummyListener : IGameEventListener {
        public EventHitCounts HitCounts = new EventHitCounts();

        public void Initialize() {
            HitCounts.Initialize++;
        }

        public void OnStartState() {
            HitCounts.OnStartState++;
        }

        public void OnDestroyState() {
            HitCounts.OnDestroyState++;
        }

        public void OnUpdate() {
            HitCounts.Update++;
        }

        public void OnFixedUpdate() {
            HitCounts.FixedUpdate++;
        }
    }
}
