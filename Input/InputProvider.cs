using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Pripizden.InputSystem;
using static UnityEngine.EventSystems.StandaloneInputModule;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pripizden.InputSystem
{
    public abstract partial class InputProvider : MonoBehaviour
    {
        [System.Serializable]
        public class OnInputModeChangedEvent : UnityEvent<InputMode> { }

        [System.Serializable]
        public class OnInputDeviceTypeChangedEvent : UnityEvent<InputDeviceType> { }


        [SerializeField]
        private bool m_singleInstance;

        //[SerializeField]
        //private bool m_simulateTouchInput = true;


        private List<InputModeRequest> m_inputModeRequests = new List<InputModeRequest>();  

        // Maps holding information about current input state.
        protected Dictionary<string, InputValue<ActionEvent>> m_actionMap;
        protected Dictionary<string, InputValue<float>> m_axisMap;
        protected Dictionary<string, InputValue<Vector2>> m_vectorMap;

        public static InputProvider Current { get; protected set; }

        public InputMode InputMode { get; private set; }

        public OnInputModeChangedEvent OnInputModeChanged = new OnInputModeChangedEvent();

        public InputDeviceType InputDeviceType { get; private set; }

        public OnInputDeviceTypeChangedEvent OnInputDeviceTypeChanged = new OnInputDeviceTypeChangedEvent();


        protected virtual void Awake()
        {
            if (Current != null && m_singleInstance)
            {
                Destroy(this.gameObject);
                return;
            }

#if UNITY_EDITOR
            /*if (m_simulateTouchInput)
                UnityEngine.InputSystem.EnhancedTouch.TouchSimulation.Enable();*/
#endif

            // Init maps, cache types
            m_actionMap = new Dictionary<string, InputValue<ActionEvent>>();
            m_axisMap = new Dictionary<string, InputValue<float>>();
            m_vectorMap = new Dictionary<string, InputValue<Vector2>>();

            Current = this;

            if (m_singleInstance)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (Current == this)
                Current = null;
        }

        protected abstract void OnInputModeChangedInternal();

        protected void ChangeInputDeviceTypeInternal(InputDeviceType inputDeviceType)
        {
#if DEBUG
            Debug.Log($"Input device type changed from {InputDeviceType} to {inputDeviceType}");
#endif

            InputDeviceType = inputDeviceType;
            OnInputDeviceTypeChanged?.Invoke(InputDeviceType);
        }

        public void ProcessInput(List<InputComponent> inputComponentList)
        {

            //iterate through the list
            foreach (var inputComponent in inputComponentList)
            {
                if (inputComponent == null || !inputComponent.bInputEnabled)
                    continue;

                //handle Action events;
                for (int j = 0; j < inputComponent.ActionBindings.Count; j++)
                {
                    var binding = inputComponent.ActionBindings[j];

                    InputValue<ActionEvent> tempInputValue;
                    if (m_actionMap.TryGetValue(binding.ActionName, out tempInputValue))
                    {
                        //if event wasn't consumed and is of an appropriate type
                        if (tempInputValue.bIsDirty && tempInputValue.Value == binding.ActionType)
                        {
                            binding.ActionDelegate?.Invoke();

                            /*//consume input only if binding is set to consume (default)
                            if (binding.bConsumeInput)
                                tempInputValue.bIsDirty = false;*/
                        }
                    }
                }

                //handle Axis events;
                for (int j = 0; j < inputComponent.AxisBindings.Count; j++)
                {
                    var binding = inputComponent.AxisBindings[j];
                    InputValue<float> tempInputValue;

                    if (m_axisMap.TryGetValue(binding.AxisName, out tempInputValue))
                    {
                        //if axis change wasn't consumed
                        if (tempInputValue.bIsDirty)
                        {
                            binding.AxisDelegate?.Invoke(tempInputValue.Value);
                            //consume input only if binding is set to consume (default)
                            /*if (binding.bConsumeInput)
                                tempInputValue.bIsDirty = false;*/
                        }
                    }

                }

                //handle Vector2 events;
                for (int j = 0; j < inputComponent.Vector2Bindings.Count; j++)
                {
                    var binding = inputComponent.Vector2Bindings[j];
                    InputValue<Vector2> tempInputValue;
                    if (m_vectorMap.TryGetValue(binding.Vector2Name, out tempInputValue))
                    {
                        if (tempInputValue.bIsDirty)
                        {
                            binding.Vector2Delegate?.Invoke(tempInputValue.Value);
                            //consume input only if binding is set to consume (default)
                            /*if (binding.bConsumeInput)
                                tempInputValue.bIsDirty = false;*/
                        }
                    }
                }
            }

            foreach (var val in m_actionMap.Values) val.UseValue();
            foreach (var val in m_axisMap.Values) val.UseValue();
            foreach (var val in m_vectorMap.Values) val.UseValue();
        }

        private void ReleaseInputModeRequest(InputModeRequest inputModeLock)
        {
            bool isRemovingLast = false;

            if (m_inputModeRequests.Last() == inputModeLock)
            {
                isRemovingLast = true;
            }

            m_inputModeRequests.Remove(inputModeLock);

            if (m_inputModeRequests.Count == 0)
            {
                InputMode = InputMode.None;
                Debug.Log($"Input mode changed to {InputMode}");
                OnInputModeChangedInternal();
                return;
            }

            if (isRemovingLast)
            {
                var last = m_inputModeRequests.Last();

                InputMode = last.InputMode;
                Debug.Log($"Input mode changed to {InputMode}");

                OnInputModeChangedInternal();

                EventSystem.current.SetSelectedGameObject(last.lastSelectedGameObject);
            }
        }

        public InputModeRequest RequestInputMode(InputMode inputMode, GameObject selectedGameObject = null)
        {
            InputModeRequest inputModeLock = new InputModeRequest(inputMode, this);

            if (m_inputModeRequests.Count > 0)
            {
                // Last request
                var lastInputModeRequest = m_inputModeRequests.Last();
                // If last request exists
                if (lastInputModeRequest != null)
                    // Saving last selected object for current input mode
                    lastInputModeRequest.lastSelectedGameObject = EventSystem.current.currentSelectedGameObject;
            }

            m_inputModeRequests.Add(inputModeLock);

            EventSystem.current.SetSelectedGameObject(selectedGameObject);

            InputMode = inputMode;
            Debug.Log($"Input mode changed to {InputMode}");

            OnInputModeChangedInternal();

            return inputModeLock;
        }

        public virtual float GetAxisValue(string axisName, float defaultValue = 0.0f)
        {
            InputValue<float> tempVal;
            if (m_axisMap.TryGetValue(axisName, out tempVal)) return tempVal.Value;
            return defaultValue;
        }

        public virtual ActionEvent GetActionState(string actionName, ActionEvent defaultState = ActionEvent.Released)
        {
            InputValue<ActionEvent> tempVal;
            if (m_actionMap.TryGetValue(actionName, out tempVal)) return tempVal.Value;
            return defaultState;
        }

        public virtual Vector2 GetVectorValue(string axisName, Vector2 defaultValue)
        {
            InputValue<Vector2> tempVal;
            if (m_vectorMap.TryGetValue(axisName, out tempVal)) return tempVal.Value;
            return defaultValue;
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(InputProvider), true)]
        public class InputProviderEditor : Editor
        {
            private InputProvider m_inputProvider;

            private void OnEnable()
            {
                m_inputProvider = target as InputProvider;
            }

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (!Application.isPlaying)
                    return;

                GUILayout.BeginVertical();

                GUILayout.Label($"Input device type: {m_inputProvider.InputDeviceType}");


                if (m_inputProvider.m_inputModeRequests != null)
                {
                    EditorGUILayout.Space(20f);

                    GUILayout.Label("Current requests:");

                    foreach (var request in m_inputProvider.m_inputModeRequests)
                    {
                        GUILayout.Label($"{request.InputMode} : {request.lastSelectedGameObject}");
                    }
                }

                if (m_inputProvider.m_actionMap != null)
                {
                    EditorGUILayout.Space(20f);

                    GUILayout.Label("Action maps:");

                    foreach (var actionMapKVP in m_inputProvider.m_actionMap)
                    {
                        GUILayout.Label($"{actionMapKVP.Key} : {actionMapKVP.Value.Value}");
                    }

                }

                if (m_inputProvider.m_axisMap != null)
                {
                    EditorGUILayout.Space(20f);

                    GUILayout.Label("Float axis maps:");

                    foreach (var floatAxisMapKVP in m_inputProvider.m_axisMap)
                    {
                        GUILayout.Label($"{floatAxisMapKVP.Key} : {floatAxisMapKVP.Value.Value}");
                    }
                }

                if (m_inputProvider.m_vectorMap != null)
                {
                    EditorGUILayout.Space(20f);

                    GUILayout.Label("Vector axis maps:");

                    foreach (var vectorAxisMap in m_inputProvider.m_vectorMap)
                    {
                        GUILayout.Label($"{vectorAxisMap.Key} : {vectorAxisMap.Value.Value}");
                    }
                }

                GUILayout.EndHorizontal();
            }
        }
#endif
    }
}