
namespace ProjectName.Core.StateMachine
{
    public interface IState
    {
        public void Exit();
    }

    public interface IEnterState : IState
    {
        public void Enter();
    }

    public interface IPayloadState<TPayload> : IState
    {
        public void Enter(TPayload payload);
    }
}
