
using System;
using UnityEngine.Events;

namespace Pripizden.DataValues
{
    [Serializable]
    public class IntUnityEvent : UnityEvent<int> { };

    public class IntEventListener : BaseEventListener<int, IntUnityEvent> { }
}
