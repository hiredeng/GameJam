using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pripizden.DataValues.Timeline
{
    [DisplayName("DataEventMarker/VoidEventMarker")]
    public class VoidEventMarker : DataEventMarker
    {
        [Header("Event Data")]

        [SerializeField]
        private VoidEvent m_event;

        public override void Invoke()
        {
            m_event.Invoke();
        }
    }
}