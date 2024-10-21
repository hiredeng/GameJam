using System;

namespace Pripizden.Gameplay.Character
{
    public interface IController
    {
        IPawn ActivePawn { get; }

        event Action<IPawn> Possessed;

        event Action<IPawn> Unpossessed;

        void Possess(IPawn targetPawn);
        void Unpossess();
    }
}