using ProjectName.Extensions;
using ProjectName.Gameplay.Interactive;
using ProjectName.Gameplay.Interactive.Types;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TempCharacter = Pripizden.Gameplay.Character.Character;

namespace ProjectName.Gameplay.WorldObject
{
    public class InteractiveObject : MonoBehaviour, IInteractive<TempCharacter>, IRequireChoice<TempCharacter>
    {
        [SerializeField]
        public InteractionPoint InteractionPoint;

        public string InteractionName => gameObject.name;

        public string InteractiveID = "Unknown";        

        public bool Active
        {
            get
            {
                var interactives = Interactives;
                return interactives.Exists() && interactives.Length > 0;
            }
        }

        public bool Occupied => false;


        public string State { get => _currentState ? _currentState.Name : ""; }

        [SerializeField]
        private List<ObjectState> _states;
        private ObjectState _currentState;

        private Interaction[] Interactives => _currentState.Exists()? _currentState.GetComponents<Interaction>() : GetComponents<Interaction>();

        public void Awake()
        {
            if(_states.Count>0)
            {
                SetState(_states.First());
            }
        }

        public void Interact(TempCharacter invoker)
        {
            var interactives = Interactives;
            if (interactives.NotExists() || interactives.Length == 0) return;
            if (interactives.Length == 1)
                interactives.First().Interact(invoker);
            else
                invoker.RequestChoice(this);
        }

        public IEnumerable<string> GetOptions()
        {
            List<string> names = new List<string>();
            foreach(var interactive in Interactives)
            {
                names.Add(interactive.InteractionName);
            }
            return names;
        }

        public void SelectOption(TempCharacter invoker, int index)
        {
            Interactives[index].Interact(invoker);
        }



        public void EnterState(string key) => 
            SetState(GetState(key));

        private ObjectState GetState(string key) =>
            _states.First(x => x.name == key);

        private void SetState(ObjectState state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }

    [UnityEditor.CustomEditor(typeof(InteractiveObject))]
    public class InteractiveObjectInspector: UnityEditor.Editor
    {
        string state;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var inter = (InteractiveObject)target;
            state = UnityEditor.EditorGUILayout.TextField(state);
            if(GUILayout.Button("Enter"))
            {
                inter.EnterState(state);
            }
        }
    }
}