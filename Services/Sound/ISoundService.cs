
using ProjectName.Core.ServiceModel;
using System;

namespace ProjectName.Services.Sound
{
    public interface ISoundService : IService
    {
        public event Action Initialized;

        float MusicVolume { get; }
        float SfxVolume { get; }
        bool MusicMuted { get; }
        bool SfxMuted { get; }

        event Action<bool> SfxMuteToggled;
        event Action<bool> MusicMuteToggled;

        void ToggleSfx();
        void ToggleMusic();
        public void SetMusicVolume(float volume);
        public void SetSfxVolume(float volume);


        void Init();
        void PlaySfx(SfxType sfxType);
    }
}