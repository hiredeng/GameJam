using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pripizden.DataValues
{
    public class VoidEventEmitter : MonoBehaviour
    {
        [SerializeField]
        private VoidEvent m_event;

        public void Emit()
        {
            m_event?.Invoke();
        }
    }
}
