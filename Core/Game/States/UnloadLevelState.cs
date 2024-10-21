using ProjectName.Services;
using ProjectName.Services.Logging;
using ProjectName.Core.StateMachine;

namespace ProjectName.Core.Game.States
{
    public class UnloadLevelState : BaseGameState, IEnterState
    {
        private readonly ILogService _logger;

        public UnloadLevelState(ILogService logger)
        {
            _logger = logger;
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }
    }
}
