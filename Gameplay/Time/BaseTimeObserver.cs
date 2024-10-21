using ProjectName.Services.Timing;
using System.Collections;
using UnityEngine;
using VContainer;

namespace ProjectName.Gameplay.Timing
{
    public class BaseTimeObserver : MonoBehaviour, ITimeObserver
    {
        ITimeKeeper _timeKeeper;

        [Inject]
        public void Construct(ITimeKeeper timeKeeper)
        {
            _timeKeeper = timeKeeper;
            _timeKeeper.AttachObserver(this);
        }

        public virtual void OnEnable()
        {
            
        }

        public virtual void OnDisable()
        {
            _timeKeeper.DetachObserver(this);
        }

        public virtual void NotifyTime(float time)
        {

        }
    }
}