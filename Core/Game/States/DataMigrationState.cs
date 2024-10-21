using ProjectName.Services;
using ProjectName.Services.Logging;
using ProjectName.Core.StateMachine;
using System.IO;

namespace ProjectName.Core.Game.States
{
    public class DataMigrationState : BaseGameState, IEnterState
    {
        private readonly ILogService _logger;

        public DataMigrationState(ILogService logger)
        {
            _logger = logger;
        }

        public void Enter()
        {
            System.IO.File.Delete(Path.Combine(UnityEngine.Application.persistentDataPath, "config.json"));
            //_logger.Log($"[DataMigrationState]::Enter", "No data to migrate, proceeding");
            _stateMachine.Enter<LoadAppState>();
        }

        public void Exit()
        {
            
        }
    }
}
