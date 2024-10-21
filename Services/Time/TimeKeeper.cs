using FMOD;
using ProjectName.Gameplay.Timing;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace ProjectName.Services.Timing
{

    public class TimeKeeper : ITimeKeeper, ITickable
    {
        public float DayTime { get => _dayTime; }
        public float DayDuration { get => _dayDuration; }
        public bool Freeze { get => !_tick; set { _tick = !value; } }

        public event Action Expired;
        
        private float _dayTime = 0f;
        private float _dayDuration = 120f;
        private bool _tick = false;
        private List<ITimeObserver> _observers = new List<ITimeObserver>();

        public void Restart(float time = 0f)
        {
            _dayTime = time;
            Freeze = false;
        }
        public void AttachObserver(ITimeObserver observer)
        {
            _observers.Add(observer);
        }
        public void DetachObserver(ITimeObserver observer)
        {
            _observers.Remove(observer);
        }

        public void ProgressTime(float deltaTime)
        {
            _dayTime += deltaTime;
            if (_dayTime > 1f)
            {
                _dayTime = 1f;
            }
            foreach (var observer in _observers)
            {
                observer.NotifyTime(_dayTime);
            }
        }

        public void Tick()
        {
            if (!_tick) return;
            ProgressTime(UnityEngine.Time.deltaTime / _dayDuration);
        }
    }
}