using Gameplay.Phone;
using ProjectName.Services.Sound;
using System;
using UnityEngine;
using VContainer;

namespace Pripizden.UI
{
    public class MainMenuPanel : MonoBehaviour
    {

        public event Action GameStarting;
        public event Action GameExiting;

        public MessagePanel TextPanel { get => _textPanel; }
        public MessagePanel ChoicePanel { get => _choicePanel; }


        [SerializeField]
        private MessagePanel _textPanel;
        [SerializeField]
        private MessagePanel _choicePanel;
        [SerializeField]
        private SoundControl _soundControl;

        [SerializeField]
        private Animator _buttonCanvasGroupAnimator;
        [SerializeField]
        private Animator _backgroundCanvasGroupAnimator;
        [SerializeField]
        private Animator _phoneCanvasGroupAnimator;
        [SerializeField]
        private Animator _choiceCanvasGroupAnimator;


        bool once = true;

        [Inject]
        public void Construct(ISoundService soundService)
        {
            _soundControl.Construct(soundService);
            enabled = true;
        }

        
        public void HideButtons()
        {
            _buttonCanvasGroupAnimator.SetBool("Opaque", false);
        }

        public void HideBackground()
        {
            _backgroundCanvasGroupAnimator.SetBool("Opaque", false);
        }

        public void HidePhone()
        {
            _phoneCanvasGroupAnimator.SetBool("Opaque", false);
        }

        public void HideChoice()
        {
            _choiceCanvasGroupAnimator.SetBool("Opaque", false);
        }

        public void Exit()
        {
            if (enabled)
            {
                GameExiting?.Invoke();
                enabled = false;
            }
        }

        public void StartGame()
        {
            if (enabled)
            {
#if UNITY_WEBGL
                //hack: this is here for webgl
                if (once)
                {
                    FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
                    FMODUnity.RuntimeManager.CoreSystem.mixerResume();
                    once = false;
                }
#endif
                GameStarting?.Invoke();
                enabled = false;
            }
        }
    }
}