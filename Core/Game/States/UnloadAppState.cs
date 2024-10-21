using ProjectName.Core.StateMachine;
using ProjectName.Core.ServiceModel;
using ProjectName.Services.Sound;
using ProjectName.Services.Configuration;
using ProjectName.Services.StaticData;

namespace ProjectName.Core.Game.States
{
    public class UnloadAppState : BaseGameState, IEnterState
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IConfigurationSaver _configurationSaver;
        private readonly ISoundService _soundService;

        public UnloadAppState(IStaticDataService staticDataService, IConfigurationSaver configurationSaver, ISoundService soundService)
        {
            _staticDataService = staticDataService;
            _configurationSaver = configurationSaver;
            _soundService = soundService;
        }

        public void Enter()
        {
            _configurationSaver.Save();
        }

        public void Exit()
        {

        }
    }
}
