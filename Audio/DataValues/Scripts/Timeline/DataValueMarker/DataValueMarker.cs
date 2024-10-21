using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pripizden.DataValues 
{
    public abstract class DataValueMarker : Marker, INotification, INotificationOptionProvider
    {
        [Header("Marker Data")]
        [SerializeField]
        private bool m_emitOnce;
        [SerializeField]
        private bool m_emitInEditor;

        NotificationFlags INotificationOptionProvider.flags =>
            (m_emitOnce ? NotificationFlags.TriggerOnce : default) |
            (m_emitInEditor ? NotificationFlags.TriggerInEditMode : default);

        public PropertyName id { get; }

        public abstract void Invoke();
    }
    
}
