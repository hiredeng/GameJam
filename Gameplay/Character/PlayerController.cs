using Pripizden.InputSystem;
using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Pripizden.Gameplay.Character
{
    [AddComponentMenu("Input 2.0/PlayerController")]
    public class PlayerController : Controller, IInputEnabledObject
    {
        [SerializeField]
        InputComponent m_startingInputComponent = null;

        List<InputComponent> m_inputComponentList = new List<InputComponent>();

        List<InputComponent> m_customInputComponents = new List<InputComponent>();

        private bool m_inputComponentsDirty = false;

        public InputComponent InputComponent { get; protected set; } = null;    // IInputEnabledObject

        public bool bInputEnabled { get; set; } = true;                         // IInputEnabledObject


        private bool m_customInponentsEnabled = true;

        public bool CustomInputComponentsEnabled
        {
            get => m_customInponentsEnabled;
            set
            {
                m_customInponentsEnabled = value;

                m_inputComponentsDirty = true;
                //RebuildInputComponents();
            }
        }

        /// <summary>
        /// Rebuilds InputComponent list in the following order<br/>
        /// InputComponents owned by: <br/>
        /// 1 - this controller<br/>
        /// 2 - currently possessed pawn 
        /// </summary>
        protected void RebuildInputComponents()
        {
            m_inputComponentList.Clear();

            if (InputComponent != null)
                m_inputComponentList.Add(InputComponent);

            if (ActivePawn != null)
                m_inputComponentList.Add(ActivePawn.InputComponent);

            if (m_customInponentsEnabled)
                m_inputComponentList.AddRange(m_customInputComponents);
        }

        protected virtual void Awake()
        {
            SetupInputComponent();
        }

        protected void Update()
        {
            ProcessInput();
        }

        protected virtual void ProcessInput()
        {
            if (m_inputComponentsDirty)
                RebuildInputComponents();

            if (InputProvider.Current)
                InputProvider.Current.ProcessInput(m_inputComponentList);
        }

        /// <summary>
        /// Sets up Input component for this controller;
        /// </summary>
        protected virtual void SetupInputComponent()
        {
            //if input component was defined, use it;
            if (m_startingInputComponent != null)
            {
                InputComponent = m_startingInputComponent;
                return;
            }

            // Create new InputComponent if required to;
            if (InputComponent == null)
            {
                //TODO: check if component exists. 
                InputComponent = this.gameObject.AddComponent<InputComponent>();
            }

            m_inputComponentsDirty = true;
        }

        /// <summary>
        /// Possesses a Pawn to take control;
        /// </summary>
        /// <param name="newPawn">Pawn to possess</param>
        public override void Possess(IPawn newPawn)
        {
            base.Possess(newPawn);

            m_inputComponentsDirty = true;
            //RebuildInputComponents();
        }

        /// <summary>
        /// Unpossesses current pawn, if any.
        /// </summary>
        public override void Unpossess()
        {
            base.Unpossess();

            m_inputComponentsDirty = true;
            //RebuildInputComponents();
        }

        public virtual void RegisterCustomInputComponent(InputComponent inputComponent)
        {
            m_customInputComponents.Add(inputComponent);

            m_inputComponentsDirty = true;
            //RebuildInputComponents();
        }

        public virtual void UnregisterCustomInputComponent(InputComponent inputComponent)
        {
            m_customInputComponents.Remove(inputComponent);

            m_inputComponentsDirty = true;
            //RebuildInputComponents();
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(PlayerController), true)]
        public class DefaultPlayerControllerEditor : Editor
        {
            private PlayerController m_playerController;

            private void OnEnable()
            {
                m_playerController = target as PlayerController;
            }

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                EditorGUILayout.BeginVertical();

                EditorGUILayout.Space(20f);

                EditorGUILayout.LabelField("Input components:");

                foreach (var inputComponent in m_playerController.m_inputComponentList)
                {
                    EditorGUILayout.LabelField($"- {inputComponent}");
                }

                EditorGUILayout.Space(20f);

                EditorGUILayout.LabelField("Custom input components:");

                foreach (var customInputComponent in m_playerController.m_customInputComponents)
                {
                    EditorGUILayout.LabelField($"- {customInputComponent}");
                }

                EditorGUILayout.EndVertical();

            }
        }
#endif
    }
}