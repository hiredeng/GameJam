using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pripizden.DataValues
{
    [DisplayName("DataValueMarker/String DataValueMarker")]
    public class StringDataValueMarker : DataValueMarker
    {
        [Header("Value data")]

        [SerializeField]
        private StringDataValue m_stringDataValue;

        [SerializeField]
        private string m_value;

        public override void Invoke()
        {
            m_stringDataValue.Value = m_value;
        }
    }
}