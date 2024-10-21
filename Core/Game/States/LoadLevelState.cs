using ProjectName.Services.Logging;
using ProjectName.Core.StateMachine;
using System;
using ProjectName.Core.Factory;
using ProjectName.UI.Services;
using UnityEngine;
using VContainer.Unity;
using ProjectName.World;
using ProjectName.Services.Timing;

namespace ProjectName.Core.Game.States
{
    public class LoadLevelState : BaseGameState, IPayloadState<string>
    {
        private readonly ViewBlocker _viewBlocker;
        private readonly SceneLoader _sceneLoader;
        private readonly LifetimeScope _diScope;
        private readonly ITimeKeeper _timeKeeper;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly ILogService _logger;

        private LifetimeScope levelScope;

        public LoadLevelState(
            ViewBlocker viewBlocker, 
            SceneLoader sceneLoader, 
            LifetimeScope diScope, 
            ITimeKeeper timeKeeper, 
            IGameFactory gameFactory, 
            IUIFactory uiFactory, 
            ILogService logger)
        {
            _viewBlocker = viewBlocker;
            _sceneLoader = sceneLoader;
            _diScope = diScope;
            _timeKeeper = timeKeeper;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _logger = logger;
        }

        public void Enter(string level)
        {
            _logger.Log($"[LoadLevelState]::Enter", $"Entering level {level}");
            levelScope = _diScope.CreateChild();
            _viewBlocker.BlockImmediate();
            _timeKeeper.Restart();
            _timeKeeper.Freeze = true;
            _sceneLoader.LoadScene(level, Loaded);

        }

        private void Loaded()
        {
            var world = _gameFactory.CreateWorld();
            _uiFactory.CreateHudRoot();

            var cc = _gameFactory.CreateCharacterController();
            var ch = _gameFactory.CreateCharacter(new Vector3(0f, -4.5f, 0f));
            cc.Possess(ch);
            _gameFactory.CreateWorldWrapper(world, ch.transform);

            _uiFactory.CreateHUD(ch);

            _timeKeeper.Restart();
            _logger.Log($"[LoadLevelState]::Loaded", $"Level loaded, unblocking");
            _viewBlocker.Unblock();
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            levelScope.Dispose();
        }
    }
}
