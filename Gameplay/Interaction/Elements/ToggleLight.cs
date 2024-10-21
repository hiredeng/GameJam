using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TempCharacter = Pripizden.Gameplay.Character.Character;

namespace ProjectName.Gameplay.Interactive.Elements
{
    [AddComponentMenu("Interaction/Toggle Light")]
    public class ToggleLight : Interaction
    {
        public override string InteractionName => "interaction_light_toggle";

        [SerializeField]
        private Light2D _light;

        public override void Interact(TempCharacter invoker)
        {
            _light.enabled = !_light.enabled;
        }
    }
}