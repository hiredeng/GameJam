using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pripizden.DataValues
{
    public class DataBehavior : PlayableBehaviour
    {
        public DataValues.BaseEvent DataEvent;
        public int A;

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