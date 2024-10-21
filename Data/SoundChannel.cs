using System;

namespace ProjectName.Data
{
    [System.Serializable]
    public class SoundChannel
    {
        public float Volume;
        public bool Mute;

        public event Action<float> VolumeChanged;
        public event Action<bool> MuteChanged;

        public SoundChannel()
        {
            Volume = .3f;
            Mute = false;
        }

        public SoundChannel(float volume, bool mute)
        {
            Volume = volume;
            Mute = mute;
        }

        public void SetVolume(float volume)
        {
            Volume = volume;
            VolumeChanged?.Invoke(Volume);
        }

        public void SetMute(bool mute)
        {
            Mute = mute;
            MuteChanged?.Invoke(mute);
        }
    }
}