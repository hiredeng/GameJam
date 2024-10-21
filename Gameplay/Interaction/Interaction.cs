using ProjectName.Gameplay.Interactive.Types;
using UnityEngine;
using TempCharacter = Pripizden.Gameplay.Character.Character;

namespace ProjectName.Gameplay.Interactive
{
    public class Interaction : MonoBehaviour, IInteractive<TempCharacter>
    {
        public virtual string InteractionName => gameObject.name;
        public bool Active => true;
        public bool Occupied => false;

        public virtual void Interact(TempCharacter invoker)
        {
            
        }
    }
}