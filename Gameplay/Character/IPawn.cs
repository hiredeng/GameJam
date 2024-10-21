using System;
using Pripizden.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pripizden.Gameplay.Character
{
    public interface IPawn
    {
        public bool bInputEnabled { get; }
        InputComponent InputComponent { get; }
        IController ActiveController { get; }


        event Action<IController> Possessed;

        event Action<IController> Unpossessed;

        void Restart();
        void PossessBy(IController controller);
        void Unpossess();
    }
}