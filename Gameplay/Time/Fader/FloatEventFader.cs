using UnityEngine;

namespace ProjectName.Gameplay.Timing
{

    public class FloatEventFader : BaseTimeObserver
    {
        [SerializeField]
        Pripizden.DataValues.FloatEvent _value;

        public override void NotifyTime(float time)
        {
            _value.Invoke(time * 100f); 
        }

    }

}