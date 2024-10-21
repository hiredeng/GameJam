using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pripizden
{
    public class SoundEventEmitter : MonoBehaviour
    {
        [SerializeField] SoundEvent Sfx = null;

        public void InvokeFromAnimator()
        {
            Invoke();
        }

        [ContextMenu("Invoke")]
        public void Invoke()
        {
            Sfx?.Invoke(transform);
        }

        public void Invoke(SoundEvent otherEvent)
        {
            otherEvent?.Invoke(transform);
        }
    }
}