using FMOD;
using ProjectName.Core.Game.States;
using ProjectName.Core.StateMachine;
using ProjectName.Services.Configuration;
using ProjectName.Services.Logging;
using VContainer.Unity;

namespace ProjectName.Core.Game
{
    public class Game : IStartable
    {
        private IStateMachine _stateMachine;
        private readonly ILogService _logger;

        public Game(IStateMachine stateMachine, ILogService logger)
        {
            _stateMachine = stateMachine; 
            _logger = logger;
        }

        public void Start()
        {
            _stateMachine.Enter<DataMigrationState>();
        }
}
}