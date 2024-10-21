using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pripizden.DataValues
{
    public abstract class BaseEventEmitter<T> : MonoBehaviour
    {
        [SerializeField]
        private AbstractEvent m_event;

        private void OnValidate()
        {
            if (m_event != null && !(m_event is BaseEvent<T>))
            {
                m_event = null;
                Debug.Log($"Event type doesn't match type {typeof(T)}");
            }
        }

        public void Emit(T arg)
        {
            if (m_event == null)
                return;

            (m_event as BaseEvent<T>)?.Invoke(arg);
        }
    }
}