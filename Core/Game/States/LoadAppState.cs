using ProjectName.Core.StateMachine;
using ProjectName.Services.Sound;
using ProjectName.Services.Configuration;
using ProjectName.Services.StaticData;
using ProjectName.Services.Screen;

namespace ProjectName.Core.Game.States
{
    public class LoadAppState : BaseGameState, IEnterState
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IConfigurationSaver _configurationSaver;
        private readonly IScreenService _screenService;
        private readonly ISoundService _soundService;

        public LoadAppState(IStaticDataService staticDataService, IConfigurationSaver configurationSaver, IScreenService screenService, ISoundService soundService)
        {
            _staticDataService = staticDataService;
            _configurationSaver = configurationSaver;
            _screenService = screenService;
            _soundService = soundService;
        }

        public void Enter()
        {
            _configurationSaver.Load();
            _staticDataService.Load();
            _screenService.Init();
            _soundService.Initialized += OnSoundInitialized;
            _soundService.Init();
        }

        public void Exit()
        {

        }

        private void OnSoundInitialized()
        {
            _soundService.Initialized -= OnSoundInitialized;
            _stateMachine.Enter<MainMenuState>();
            OnInitialized();
        }

        private void OnInitialized()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged += OnEditorPlaymodeChange;
#endif
            UnityEngine.Application.wantsToQuit += OnApplicationQuit;
        }

        private bool OnApplicationQuit()
        {
            _stateMachine.Enter<UnloadAppState>();
            return true;
        }

#if UNITY_EDITOR
        private void OnEditorPlaymodeChange(UnityEditor.PlayModeStateChange playmodeChange)
        {
            if (playmodeChange == UnityEditor.PlayModeStateChange.ExitingPlayMode)
            OnApplicationQuit();
        }
#endif
    }
}
