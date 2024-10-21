using ProjectName.Gameplay.WorldObject;
using UnityEngine;
using TempCharacter = Pripizden.Gameplay.Character.Character;

namespace ProjectName.Gameplay.Interactive.Elements
{
    [AddComponentMenu("Interaction/SwitchState")]
    public class SwitchState : InteractionNamed
    {
        [SerializeField]
        private string _stateName;

        [SerializeField]
        private InteractiveObject _targetObject;

        public override void Interact(TempCharacter invoker)
        {
            _targetObject.EnterState(_stateName);
        }
    }
}