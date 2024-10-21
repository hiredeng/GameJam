using System;
using UnityEngine;
using UnityEngine.Events;

namespace Pripizden.DataValues
{
    [Serializable]
    public class FloatUnityEvent : UnityEvent<float> { }

    public class FloatEventListener : BaseEventListener<float, FloatUnityEvent> { }
}