using ProjectName.Services;
using ProjectName.Services.Logging;
using ProjectName.Core.StateMachine;

namespace ProjectName.Core.Game.States
{
    public class GameLoopState : BaseGameState, IEnterState
    {
        private readonly ILogService _logger;

        public GameLoopState(ILogService logger)
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
