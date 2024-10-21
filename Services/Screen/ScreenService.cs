using ProjectName.Data;
using ProjectName.Services.Configuration;

namespace ProjectName.Services.Screen
{
    public class ScreenService : IScreenService
    {
        private readonly IConfigurationService _configurationService;
        private ScreenConfiguration _screenConfiguration;

        public int FrameRate 
        {
            get => _screenConfiguration.TargetFramerate;
            set
            {
                UnityEngine.Application.targetFrameRate = value;
                _screenConfiguration.TargetFramerate = value;
            }
        } 
        public bool VSync
        {
            get => _screenConfiguration.VerticalSync;
            set
            {
                UnityEngine.QualitySettings.vSyncCount = value ? 1 : 0;
                _screenConfiguration.VerticalSync = value;
            }
        }

        public ScreenService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public void Init()
        {
            _screenConfiguration = _configurationService.Configuration.ScreenConfig;
            UnityEngine.Application.targetFrameRate = _screenConfiguration.TargetFramerate;
            UnityEngine.QualitySettings.vSyncCount = _screenConfiguration.VerticalSync ? 1 : 0;
        }
    }
}