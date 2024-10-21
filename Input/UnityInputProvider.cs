using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Pripizden.InputSystem
{   
    [RequireComponent(typeof(PlayerInput))]
    public class UnityInputProvider : InputProvider
    {
        /// <summary>
        /// Reference to inputsystem component
        /// </summary>
        PlayerInput m_playerInput;

        void OnEnable()
        {
            //subscribe to input events
            m_playerInput = GetComponent<PlayerInput>();
            m_playerInput.onActionTriggered -= PlayerInput_onActionTriggered;
            m_playerInput.onActionTriggered += PlayerInput_onActionTriggered;
        }

        void OnDisable()
        {
            //unsubscribe from input events
            m_playerInput.onActionTriggered -= PlayerInput_onActionTriggered;
        }


        void Update()
        {
            //Flush input events queue;
            UnityEngine.InputSystem.InputSystem.Update();
        }

        protected override void OnInputModeChangedInternal()
        {
            //TODO:   
        }

        void PlayerInput_onActionTriggered(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (!Application.isFocused) return;
            // fill appropriate input maps with values;
            switch(obj.action.type)
            {
                // axis/vector values
                case InputActionType.Value:
                    {
                        // TODO: Optimize if to switch
                        // Handle axis(float) input;
                        if (obj.valueType == typeof(float))
                        {
                            if (!m_axisMap.ContainsKey(obj.action.name))
                            {
                                m_axisMap.Add(obj.action.name, new InputValue<float>(0.0f, false));
                            }
                            m_axisMap[obj.action.name].SetValue(obj.ReadValue<float>());
                        }
                        // Handle Vector2 input;
                        else if (obj.valueType == typeof(Vector2))
                        {
                            if (!m_vectorMap.ContainsKey(obj.action.name))
                            {
                                m_vectorMap.Add(obj.action.name, new InputValue<Vector2>(Vector2.zero, false));
                            }
                            m_vectorMap[obj.action.name].SetValue(obj.ReadValue<Vector2>());
                        }
                        else if (obj.valueType == typeof(TouchState))
                        {
                            if(!m_vectorMap.ContainsKey(obj.action.name))
                            {
                                m_vectorMap.Add(obj.action.name, new InputValue<Vector2>(Vector2.zero, false));
                            }
                            var touchState = obj.ReadValue<TouchState>();
                            if(touchState.isTap)
                            {
                                m_vectorMap[obj.action.name].SetValue(touchState.position);
                            }
                        }
                    }break;
                //buttons
                case InputActionType.Button:
                    {
                        //Handle action input;
                        if (!m_actionMap.ContainsKey(obj.action.name))
                        {
                            m_actionMap.Add(obj.action.name, new InputValue<ActionEvent>(ActionEvent.Released, false));
                        }

                        var action = m_actionMap[obj.action.name];

                        switch(action.Value)
                        {
                            case ActionEvent.Released:
                                {
                                    if (obj.action.triggered) action.SetValue(ActionEvent.Pressed);
                                }
                                break;
                            case ActionEvent.Pressed:
                                {
                                    if (!obj.action.triggered) action.SetValue(ActionEvent.Released);
                                }break;

                            default: throw new NotImplementedException("[InputProvider] Action event type not supported : " + action.Value.ToString());
                        }
                    }
                    break;
            }
        }
    }
}
#endif