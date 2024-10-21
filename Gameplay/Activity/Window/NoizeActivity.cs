using Pripizden.Gameplay.Parameter;
using UnityEngine;

namespace Pripizden.Gameplay.Activity
{
    public class NoizeActivity : BaseDistraction
    {
        ParameterContainer _param;
        ParameterValue _productivity;

        [SerializeField]
        float penalty = 0.2f;

        [SerializeField]
        private WindowItself _window;


        [SerializeField]
        SoundEvent _constructionOn;
        [SerializeField]
        SoundEvent _constructionOff;

        protected override void Restart()
        {
            _param = FindObjectOfType<ParameterContainer>();
            _productivity = _param.GetParameter(Parameters.Productivity);
            _window.SetState(WindowItself.Openness.Open);
            _constructionOff?.Invoke();
        }

        public override bool IsAvailable()
        {
            return _window.State != WindowItself.Openness.Closed;
        }

        public override void Activate()
        {
            base.Activate();
            _constructionOn?.Invoke();
            _window.SetState( WindowItself.Openness.Noizy);
            _window.Closed.AddListener(WindowClosed);
            _productivity.Value -= penalty;
        }

        public override void Clear()
        {
            base.Clear();
            _constructionOff?.Invoke();
            _window.Closed.RemoveListener(WindowClosed);
            _productivity.Value += penalty;
        }

        private void WindowClosed()
        {
            Clear();
        }

        public override void DoAutomatically()
        {
            
        }
    }
}