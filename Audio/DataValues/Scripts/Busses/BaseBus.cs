using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pripizden.DataValues
{
    public abstract class AbstractBus : ScriptableObject { }

    public abstract class BaseBus<T> : AbstractBus
    {
        public delegate T BusDelegate();

        private BusDelegate m_busAction = null;

        public bool IsActionAssigned => m_busAction != null;

        public void AssignAction(BusDelegate delegateAction) => m_busAction = delegateAction;

        public void ClearAction() => m_busAction = null;

        public T Invoke()
        {
            return m_busAction.Invoke();
        }
    }
}