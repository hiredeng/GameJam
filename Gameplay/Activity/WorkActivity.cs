using Pripizden.Gameplay.Character;
using Pripizden.Gameplay.Parameter;
using Pripizden.Service;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Pripizden.Gameplay.Activity
{
    public class WorkActivity : BaseDistraction
    {
        ParameterContainer _param;
        ParameterValue _workProgress;
        ParameterValue _productivity;

        [SerializeField]
        float timeToFinishWork = 90f;
        float workSpeed = 0f;

        [SerializeField]
        CallbackInteractive WorkInteractive;

        bool _catPresent = false;

        [SerializeField]
        SoundEvent _workStart;

        [SerializeField]
        SoundEvent _workStop;

        protected override void Restart()
        {
            workSpeed = 1f / timeToFinishWork;
            _param = FindObjectOfType<ParameterContainer>();
            _productivity = _param.GetParameter(Parameters.Productivity);
            _workProgress = _param.GetParameter(Parameters.WorkProgress);
            _catPresent = false;
            WorkInteractive.Active = false;
            _workStop?.Invoke();
        }

        public override void Activate()
        {
            base.Activate();
            WorkInteractive.Active = true;
            WorkInteractive.Captive = true;
            WorkInteractive.Construct(WorkTouched, WorkReleased);
        }

        public override void Clear()
        {
            base.Clear();
            WorkInteractive.Active = false;
        }

        private void Update()
        {
            if (_catPresent)
                _workProgress.Value += _productivity.Value*Time.deltaTime*workSpeed;
        }

        private void WorkTouched()
        {
            _workStart?.Invoke();
            _catPresent = true;
        }

        private void WorkReleased()
        {
            _workStop?.Invoke();
            _catPresent = false;
        }

        public override void DoAutomatically()
        {
            
        }

    }
}