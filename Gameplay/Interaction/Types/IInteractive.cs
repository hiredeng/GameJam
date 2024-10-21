using UnityEngine;

namespace ProjectName.Gameplay.Interactive.Types
{
    public interface IInteractiveBase
    {
        string InteractionName { get; }
        //lowercase because Unity hates naming conventions
        GameObject gameObject { get; }

        bool Active { get; }
        bool Occupied { get; }
    }

    public interface IInteractive: IInteractiveBase
    {
        void Interact();
    }

    public interface IInteractive<TInteractor> : IInteractiveBase where TInteractor : class 
    {
        public void Interact(TInteractor invoker);
    }
}