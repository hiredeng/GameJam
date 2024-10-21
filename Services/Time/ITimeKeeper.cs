using ProjectName.Gameplay.Timing;
using System;

namespace ProjectName.Services.Timing
{
    public interface ITimeKeeper
    {
        float DayDuration { get; }
        float DayTime { get; }
        bool Freeze { get; set; }

        event Action Expired;

        void AttachObserver(ITimeObserver observer);
        void DetachObserver(ITimeObserver observer);
        void Restart(float time = 0);
        void ProgressTime(float deltaTime);
    }
}