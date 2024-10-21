using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectName.Gameplay.Timing
{
    public class SoundVolumeFader : BaseTimeObserver
    {
        [SerializeField]
        AudioSource m_audioSource;

        [SerializeField]
        private AnimationCurve m_curve;

        public override void NotifyTime(float time)
        {
            m_audioSource.volume = m_curve.Evaluate(time);
        }

    }
}