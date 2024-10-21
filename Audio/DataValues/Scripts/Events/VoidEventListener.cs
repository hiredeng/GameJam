using System;
using UnityEngine.Events;

namespace Pripizden.DataValues
{

    [Serializable]
    public class VoidUnityEvent : UnityEvent { }

    public class VoidEventListener : BaseEventListener<VoidUnityEvent> { }
}