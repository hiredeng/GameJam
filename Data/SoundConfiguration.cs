namespace ProjectName.Data
{
    [System.Serializable]
    public class SoundConfiguration
    {
        public SoundChannel Master;
        public SoundChannel Music;
        public SoundChannel Sfx;

        public SoundConfiguration()
        {
            Master = new SoundChannel();
            Music = new SoundChannel();
            Sfx = new SoundChannel();
        }
    }
}