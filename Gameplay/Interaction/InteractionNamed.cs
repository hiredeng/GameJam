using ProjectName.Gameplay.Interactive.Types;
using UnityEngine;
using TempCharacter = Pripizden.Gameplay.Character.Character;

namespace ProjectName.Gameplay.Interactive
{
    public class InteractionNamed : Interaction, IInteractive<TempCharacter>
    {
        [SerializeField]
        protected string _interactionName = "interaction_";
        public override string InteractionName => _interactionName;

        public override void Interact(TempCharacter invoker)
        {

        }
    }
}