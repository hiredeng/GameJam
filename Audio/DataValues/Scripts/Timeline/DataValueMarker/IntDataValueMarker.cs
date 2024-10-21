using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pripizden.DataValues
{
    [DisplayName("DataValueMarker/Int DataValueMarker")]
    public class IntDataValueMarker : DataValueMarker
    {
        [Header("Value data")]

        [SerializeField]
        private IntDataValue m_intDataValue;

        [SerializeField]
        private int m_value;

        public override void Invoke()
        {
            m_intDataValue.Value = m_value;
        }
    }

}