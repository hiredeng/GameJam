using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pripizden.DataValues.Timeline
{
    [DisplayName("DataEventMarker/BoolDataEventMarker")]
    public class BoolDataEventMarker : DataEventMarker
    {
        [Header("Event Data")]

        [SerializeField]
        private BoolEvent m_event;

        [SerializeField]
        private bool m_invokeValue;

        public override void Invoke() => m_event.Invoke(m_invokeValue);
    }
}