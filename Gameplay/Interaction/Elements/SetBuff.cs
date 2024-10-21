using ProjectName.Inspector;
using UnityEngine;
using UnityEngine.Serialization;
using TempCharacter = Pripizden.Gameplay.Character.Character;

namespace ProjectName.Gameplay.Interactive.Elements
{
    [AddComponentMenu("Interaction/SetBuff")]
    public class SetBuff : InteractionNamed
    {
        [SerializeField]
        [StatName]
        private string _parameterName;
        [FormerlySerializedAs("_parameterValue")]
        [SerializeField]
        private float _setValue;

        public override void Interact(TempCharacter invoker)
        {
            invoker.Stats.AddParameter(_parameterName, _setValue);
        }
    }
}