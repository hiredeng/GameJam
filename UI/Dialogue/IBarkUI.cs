namespace ProjectName.UI
{
    public interface IBarkUI
    {
        void Bark(Subtitle subtitle);
        void Hide();
        bool isPlaying { get; }
    }

}
