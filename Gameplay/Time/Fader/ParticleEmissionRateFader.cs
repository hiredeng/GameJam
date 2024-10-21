using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectName.Gameplay.Timing
{
    public class ParticleEmissionRateFader : BaseTimeObserver
    {
        [SerializeField]
        ParticleSystem m_particleSystem;

        [SerializeField]
        private AnimationCurve m_curve;

        public override void NotifyTime(float time)
        {
            var emission = m_particleSystem.emission;
            float rate = m_curve.Evaluate(time);
            emission.rateOverTime = rate;
            if (!m_particleSystem.isPlaying && rate > 0f) m_particleSystem.Play();
        }

    }
}