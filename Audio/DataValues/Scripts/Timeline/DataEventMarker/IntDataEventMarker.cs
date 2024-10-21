using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pripizden.DataValues.Timeline
{
    [DisplayName("DataEventMarker/Int DataEventMarker")]
    public class IntDataEventMarker : DataEventMarker
    {
        [Header("Event Data")]

        [SerializeField]
        private IntEvent m_event;

        [SerializeField]
        private int m_invokeValue;

        public override void Invoke() => m_event.Invoke(m_invokeValue);
    }
}