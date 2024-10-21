using ProjectName.Core.StateMachine;
using ProjectName.Services.Logging;
using ProjectName.UI.Services;
using Pripizden.UI;
using System;
using ProjectName.Services.Sound;

namespace ProjectName.Core.Game.States
{
    public class MainMenuState : BaseGameState, IEnterState
    {
        private readonly ViewBlocker _viewBlocker;
        private readonly SceneLoader _sceneLoader;
        private readonly ISoundService _soundService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ILogService _logger;
        private readonly IUIFactory _uiFactory;

        private const string MenuSceneName = "Menu";
        private const string LogTag = "[MainMenuState]";

        private MainMenuPanel _menuPanel;

        public MainMenuState(ViewBlocker viewBlocker, SceneLoader sceneLoader, ISoundService soundService, ICoroutineRunner coroutineRunner,  ILogService logger, IUIFactory uiFactory)
        {
            _viewBlocker = viewBlocker;
            _sceneLoader = sceneLoader;
            _soundService = soundService;
            _coroutineRunner = coroutineRunner;
            _logger = logger;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _viewBlocker.Block();
            _logger.Log(LogTag, "Starting main menu transition");
            _sceneLoader.LoadScene(MenuSceneName, Loaded);
        }

        private void Loaded()
        {
            _viewBlocker.Unblock();
            _logger.Log(LogTag, "Main menu transition ended");
            _uiFactory.CreateUIRoot();
            _logger.Log(LogTag, "UIRootCreated");
            _menuPanel = _uiFactory.CreateMainMenu();
            _menuPanel.GameStarting += OnGameStartRequested;
            _menuPanel.GameExiting += OnGameExitRequested;
            _logger.Log(LogTag, "MainMenuCreated");
        }

        private void OnGameExitRequested()
        {
            UnityEngine.Application.Quit();
        }

        private void OnGameStartRequested()
        {
            var handler = new IntroductoryDialogueHandler(_coroutineRunner, _soundService, _menuPanel);
            handler.LevelStarting += LoadLevel;
            handler.TutorialStarting += LoadLevel;
            _coroutineRunner.StartCoroutine(handler.StartGameProcess());
        }

        public void Exit()
        {
            _menuPanel.GameStarting -= OnGameStartRequested;
            _menuPanel.GameExiting -= OnGameExitRequested;
            UnityEngine.Object.Destroy(_menuPanel.gameObject);
            _menuPanel = null;
        }

        public void LoadLevel()
        {
            _logger.Log(LogTag, $"Level transition requesting. Loading player progress");
            _stateMachine.Enter<LoadPlayerProgressState>();
        }
    }
}
