using ProjectName.Core.StateMachine;
using ProjectName.Services.Logging;

namespace ProjectName.Core.Game.States
{
    public class LoadPlayerProgressState : BaseGameState, IEnterState
    {
        private const string LevelSceneName = "Level";

        private readonly ILogService _logger;

        public LoadPlayerProgressState(ILogService logger)
        {
            _logger = logger;
        }

        public void Enter()
        {
            _logger.Log("[LoadPlayerProgressState]", $"No player progress to load, loading initial level ({LevelSceneName})");
            _stateMachine.Enter<LoadLevelState, string>(LevelSceneName);
        }

        public void Exit()
        {

        }
    }
}
