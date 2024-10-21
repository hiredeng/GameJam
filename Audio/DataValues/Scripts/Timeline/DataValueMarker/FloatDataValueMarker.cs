using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pripizden.DataValues
{
    [DisplayName("DataValueMarker/Float DataValueMarker")]
    public class FloatDataValueMarker : DataValueMarker
    {
        [Header("Value data")]

        [SerializeField]
        private FloatDataValue m_floatDataValue;

        [SerializeField]
        private float m_value;

        public override void Invoke()
        {
            m_floatDataValue.Value = m_value;
        }
    }
}