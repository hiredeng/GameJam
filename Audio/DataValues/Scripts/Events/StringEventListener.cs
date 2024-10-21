using System;
using UnityEngine.Events;

namespace Pripizden.DataValues
{
    [Serializable]
    public class StringUnityEvent : UnityEvent<string> { };

    public class StringEventListener : BaseEventListener<string, StringUnityEvent> { }
}
