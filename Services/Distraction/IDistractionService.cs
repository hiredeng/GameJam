using Pripizden.Gameplay.Activity;
using ProjectName.Core.ServiceModel;
using System;

namespace ProjectName.Services.Distraction
{
    public interface IDistractionService : IService
    {
        public void Clear();
        public event Action<BaseDistraction> DistractionAppeared;
        public event Action<BaseDistraction> DistractionCleared;
        public void RegisterDistraction(BaseDistraction activity);
        public void ClearDistraction(BaseDistraction activity);
    }
}