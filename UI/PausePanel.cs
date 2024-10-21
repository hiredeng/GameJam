using ProjectName.Services.Sound;
using UnityEngine;
using VContainer;

namespace Pripizden.UI
{
    public class PausePanel : WindowBase
    {
        [SerializeField]
        private SoundControl _soundControl;

        ISoundService _soundService;

        [Inject]
        public void Construct(ISoundService soundService)
        {
            _soundService = soundService;
        }


        protected override void OnAwake()
        {
        }

        protected override void Initialize()
        {
            Time.timeScale = 0f;

            _soundControl.Construct(_soundService);
        }

        public void Close()
        {
            Time.timeScale = 1f;
            Destroy(gameObject);
        }


        public void Quit()
        {
            Time.timeScale = 1f;
            Application.Quit();
        }
    }
}