using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TempCharacter = Pripizden.Gameplay.Character.Character;

namespace ProjectName.Gameplay.Interactive.Elements
{
    [AddComponentMenu("Interaction/Describe")]
    public class Describe : Interaction
    {
        [SerializeField]
        private string _conversationKey;
        public override string InteractionName => "interaction_describe";

        public override void Interact(TempCharacter invoker)
        {
            invoker.Bark(_conversationKey);
        }
    }
}