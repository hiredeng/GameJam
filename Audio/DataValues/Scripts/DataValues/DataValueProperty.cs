using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Pripizden.DataValues
{

    [Serializable]
    public abstract class DataValueProperty<T>
    {
        public bool useDataValue;

        [SerializeField]
        private float m_value;

        [SerializeField]
        private BaseDataValue<T> m_dataValue;
    }

}