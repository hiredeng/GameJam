using Pripizden.Gameplay.Character;
using Pripizden.Gameplay.Parameter;
using Pripizden.Service;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Pripizden.Gameplay.Activity
{
    public class FreshAirActivity : BaseDistraction
    {
        ParameterContainer _param;
        ParameterValue _productivity;

        [SerializeField]
        float penalty = 0.2f;

        [SerializeField]
        private WindowItself _window;

        [SerializeField]
        SoundEvent _hot = null;

        protected override void Restart()
        {
            _param = FindObjectOfType<ParameterContainer>();
            _productivity = _param.GetParameter(Parameters.Productivity);
            _window.Opened.RemoveListener(WindowOpened);
            _window.SetState(WindowItself.Openness.Open);
        }

        public override bool IsAvailable()
        {
            return _window.State == WindowItself.Openness.Closed;
        }

        public override void Activate()
        {
            _hot?.Invoke();
            base.Activate();
            _window.Opened.AddListener(WindowOpened);
            _productivity.Value -= penalty;
        }

        public override void Clear()
        {
            base.Clear();
            _window.Opened.RemoveListener(WindowOpened);
            _productivity.Value += penalty;
        }

        private void WindowOpened()
        {
            Clear();
        }

        public override void DoAutomatically()
        {
        }
    }
}