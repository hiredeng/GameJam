namespace ProjectName.Gameplay.Interactive.Types
{
    public interface ICaptive
    {

    }

    public interface ICaptive<TInteractor> : ICaptive where TInteractor : class
    {
        void Enter(TInteractor invoker);
        void Leave(TInteractor invoker);

        bool IsMovementAllowed();
    }
}
