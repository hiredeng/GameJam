using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pripizden.DataValues
{
    public abstract class AbstractEvent : ScriptableObject { }

    public abstract class BaseEvent<T> : AbstractEvent
    {
        private HashSet<Action<T>> m_handlers = new HashSet<Action<T>>();
        private HashSet<IEventListener<T>> m_listeners = new HashSet<IEventListener<T>>();

        public void AddListener(IEventListener<T> listener) => m_listeners.Add(listener);

        public void RemoveListener(IEventListener<T> listener) => m_listeners.Remove(listener);

        public void AddListener(Action<T> handler) => m_handlers.Add(handler);

        public void RemoveListener(Action<T> handler) => m_handlers.Remove(handler);

        public void RemoveAllListeners()
        {
            m_handlers.Clear();
            m_listeners.Clear();
        }

        public void Invoke(T arg)
        {
            foreach(var handler in m_handlers)
            {
                handler?.Invoke(arg);
            }

            foreach(var listener in m_listeners)
            {
                listener.Invoke(arg);
            }
        }
    }

    [Serializable]
    public abstract class BaseEvent : AbstractEvent
    {
        private HashSet<Action> m_handlers = new HashSet<Action>();
        private HashSet<IEventListener> m_listeners = new HashSet<DataValues.IEventListener>();

        public void AddListener(IEventListener listener) => m_listeners.Add(listener);

        public void AddListener(Action listener) => m_handlers.Add(listener);

        public void RemoveListener(IEventListener listener) => m_listeners.Remove(listener);

        public void RemoveListener(Action listener) => m_handlers.Remove(listener);

        public void RemoveAllListeners()
        {
            m_handlers.Clear();
            m_listeners.Clear();
        }

        public void Invoke()
        {
            foreach (var handler in m_handlers)
            {
                handler?.Invoke();
            }

            foreach (var listener in m_listeners)
            {
                listener.Invoke();
            }
        }
    }

}