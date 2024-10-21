using System;
using UnityEngine;
using UnityEngine.Events;

namespace Pripizden.DataValues
{
    [Serializable]
    public class GameObjectUnityEvent : UnityEvent<GameObject> { }

    public class GameObjectEventListener : BaseEventListener<GameObject, GameObjectUnityEvent> {}
}