using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pripizden.DataValues.Timeline
{
    [DisplayName("DataEventMarker/StringDataEventMarker")]
    public class StringDataEventMarker : DataEventMarker
    {
        [Header("Event Data")]

        [SerializeField]
        private StringEvent m_event;

        [SerializeField]
        private string m_invokeValue;

        public override void Invoke() => m_event.Invoke(m_invokeValue);
    }
}