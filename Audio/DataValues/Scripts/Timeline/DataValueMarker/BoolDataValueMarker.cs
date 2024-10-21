using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pripizden.DataValues
{
    [DisplayName("DataValueMarker/Bool DataValueMarker")]
    public class BoolDataValueMarker : DataValueMarker
    {
        [Header("Value data")]

        [SerializeField]
        private BoolDataValue m_boolDataValue;

        [SerializeField]
        private bool m_value;

        public override void Invoke()
        {
            m_boolDataValue.Value = m_value;
        }
    }
}