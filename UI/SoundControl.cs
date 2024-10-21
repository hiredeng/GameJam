using ProjectName.Services.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Pripizden.UI
{
    public class SoundControl : MonoBehaviour
    {
        [SerializeField]
        Slider _musicSlider;
        [SerializeField]
        Slider _sfxSlider;

        [SerializeField]
        Button _musicMute;
        [SerializeField]
        Button _sfxMute;

        [SerializeField]
        GameObject _musicMuteIcon;
        [SerializeField]
        GameObject _sfxMuteIcon;

        ISoundService _soundService;

        public void Construct(ISoundService service)
        {
            _soundService = service;
            UpdateVisual();
            _musicMute.onClick.AddListener(OnMusicClick);
            _sfxMute.onClick.AddListener(OnSfxClick);
            _musicSlider.onValueChanged.AddListener(OnMusicChange);
            _sfxSlider.onValueChanged.AddListener(OnSfxChange);
        }
        private void OnMusicClick()
        {
            _soundService.ToggleMusic();
            UpdateVisual();
        }
        private void OnSfxClick()
        {
            _soundService.ToggleSfx();
            UpdateVisual();
        }
        private void OnMusicChange(float v)
        {
            _soundService.SetMusicVolume(v);
            UpdateVisual();
        }
        private void OnSfxChange(float v)
        {
            _soundService.SetSfxVolume(v);
            UpdateVisual();
        }


        public void UpdateVisual()
        {
            _musicSlider.value = _soundService.MusicVolume;
            _sfxSlider.value = _soundService.SfxVolume;
            _musicMuteIcon.SetActive(_soundService.MusicMuted);
            _sfxMuteIcon.SetActive(_soundService.SfxMuted);
        }

    }
}