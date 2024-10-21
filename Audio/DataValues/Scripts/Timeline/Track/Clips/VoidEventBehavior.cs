using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pripizden.DataValues
{
    public class VoidEventBehavior : PlayableBehaviour
    {
        public VoidEvent DataEvent;

        public void Fire()
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                DataEvent?.Invoke();
                Debug.Log("Fired");
            }
#else
            DataEvent?.Invoke();
#endif
        }
    }
}