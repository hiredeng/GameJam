using UnityEngine;
using UnityEngine.Events;

namespace Pripizden.DataValues
{
    public interface IEventListener
    {
        void Invoke();
    }

    public interface IEventListener<T>
    {
        void Invoke(T arg);
    }

    public abstract class AbstractEventListener : MonoBehaviour { }

    public abstract class BaseEventListener<T, TEventType> : AbstractEventListener, IEventListener<T> where TEventType : UnityEvent<T>
    {
        [SerializeField]
        private AbstractEvent m_event;

        [SerializeField]
        private TEventType m_eventResponse;

        void OnEnable()
        {
            (m_event as BaseEvent<T>)?.AddListener(this);
        }

        void OnDisable()
        {
            (m_event as BaseEvent<T>)?.RemoveListener(this);
        }

        private void OnValidate()
        {
            if (m_event != null && !(m_event is BaseEvent<T>))
            {
                m_event = null;
                Debug.Log($"Event type doesn't match type {typeof(T)}");
            }
        }

        public void Invoke(T arg)
        {
            m_eventResponse?.Invoke(arg);
        }
    }

    public abstract class BaseEventListener<TEventType> : AbstractEventListener, IEventListener where TEventType : UnityEvent
    {
        [SerializeField]
        private BaseEvent m_event;

        [SerializeField]
        private TEventType m_eventResponse;

        void OnEnable()
        {
            m_event?.AddListener(this);
        }

        void OnDisable()
        {
            m_event?.RemoveListener(this);
        }

        public void Invoke()
        {
            m_eventResponse?.Invoke();
        }
    }
}
