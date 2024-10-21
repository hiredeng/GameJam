using System;
using UnityEngine.Events;

namespace Pripizden.DataValues
{
    [Serializable]
    public class BoolUnityEvent : UnityEvent<bool> { }

    public class BoolEventListener : BaseEventListener<bool, BoolUnityEvent> { }
}
