using Pripizden.Gameplay.Activity;
using System;
using System.Collections.Generic;

namespace ProjectName.Services.Distraction
{
    public class DistractionService : IDistractionService
    {
        private List<BaseDistraction> _activities = new List<BaseDistraction>();
        public event Action<BaseDistraction> DistractionAppeared;
        public event Action<BaseDistraction> DistractionCleared;

        public void Clear()
        {
            foreach(var activity in _activities)
            {
                DistractionCleared?.Invoke(activity);
            }
            _activities.Clear();
        }
        public void RegisterDistraction(BaseDistraction activity)
        {
            _activities.Add(activity);
            DistractionAppeared?.Invoke(activity);
        }
        public void ClearDistraction(BaseDistraction activity)
        {
            _activities.Remove(activity);
            DistractionCleared?.Invoke(activity);
        }
    }
}