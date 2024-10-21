using Pripizden.Gameplay.Character;
using ProjectName.Gameplay.Interactive;
using ProjectName.Gameplay.Interactive.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pripizden.Gameplay
{
    public interface IInteractive
    {
        public Vector3 GetInteractionPosition();
        public InteractionDirection Direction { get; }
        public bool Escapeable { get; set; }
        public bool Captive { get; set; }
        public bool Active { get; set; }
        public bool CanInteract(IPawn interactor);
        public void Interact(IPawn interactor);
        public void Release(IPawn interactor);
    }
}