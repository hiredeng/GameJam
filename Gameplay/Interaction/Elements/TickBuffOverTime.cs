using ProjectName.Gameplay.Interactive.Types;
using ProjectName.Inspector;
using UnityEngine;
using TempCharacter = Pripizden.Gameplay.Character.Character;

namespace ProjectName.Gameplay.Interactive.Elements
{
    [AddComponentMenu("Interaction/TickBuffOverTime")]
    public class TickBuffOverTime : InteractionNamed, ICaptive<TempCharacter>
    {
        [SerializeField]
        [StatName]
        private string _parameterName;
        
        [SerializeField]
        private float _maxValue;
        [SerializeField]
        [Tooltip("in units per second")]
        private float _rate;

        

        public override void Interact(TempCharacter invoker)
        {
            var stats = invoker.Stats;
            if (!stats.HasParameter(_parameterName)) stats.AddParameter(_parameterName, 0f);
            invoker.EnterCaptive(this);
        }

        public void Enter(TempCharacter invoker)
        {
            UnityEngine.Debug.Log("Enter");
        }

        public void Leave(TempCharacter invoker)
        {
            UnityEngine.Debug.Log("Exit");
        }

        public bool IsMovementAllowed() => true;
    }
}