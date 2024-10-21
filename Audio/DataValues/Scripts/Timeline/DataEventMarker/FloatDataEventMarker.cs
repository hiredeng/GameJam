using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pripizden.DataValues.Timeline
{
    [DisplayName("DataEventMarker/Float DataEventMarker")]
    public class FloatDataEventMarker : DataEventMarker
    {
        [Header("Event Data")]

        [SerializeField]
        private FloatEvent m_event;

        [SerializeField]
        private float m_invokeValue;

        public override void Invoke() => m_event.Invoke(m_invokeValue);
    }
}