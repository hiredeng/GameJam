using ProjectName.Core.StateMachine;

namespace ProjectName.Core.Game
{
    public abstract class BaseGameState
    {
        protected GameStateMachine _stateMachine;

        public void SetStateMachine(GameStateMachine target)
        {
            _stateMachine = target;
        }
    }
}