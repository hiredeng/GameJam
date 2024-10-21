using ProjectName.Core.ServiceModel;

namespace ProjectName.Services.Screen
{
    public interface IScreenService : IService
    {
        int FrameRate { get; set; }
        bool VSync { get; set; }

        void Init();
    }
}