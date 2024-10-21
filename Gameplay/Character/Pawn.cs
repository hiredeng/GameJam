using Pripizden.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;


namespace Pripizden.Gameplay.Character
{
    public class Pawn : MonoBehaviour, IPawn, IInputEnabledObject
    {
        /// <summary>
        /// Input component defined in editor;
        /// </summary>
        [SerializeField]
        InputComponent m_StartingInputComponent = null;

        public event Action<IController> Possessed;

        public event Action<IController> Unpossessed;

        [SerializeField]
        UnityEngine.Events.UnityEvent PossessedEvent = null;

        [SerializeField]
        UnityEngine.Events.UnityEvent UnpossessedEvent = null;
        public InputComponent InputComponent { get; protected set; } = null;  //IInputEnabledObject

        public bool bInputEnabled { get { return InputComponent != null && InputComponent.bInputEnabled; } set { if (InputComponent) InputComponent.bInputEnabled = value; } } //IInputEnabledObject

        /// <summary>
        /// Controller this pawn is being possessed by;
        /// </summary>
        public IController ActiveController { get; protected set; }

        protected bool m_inputComponentReady = false;

        /// <summary>
        /// Invoked when this pawn is possessed by a controller
        /// </summary>
        /// <param name="controller">controller possessing this pawn</param>
        public virtual void PossessBy(IController controller)
        {
            ActiveController = controller;
            if (!m_inputComponentReady)
            {
                SetupInputComponent();
            }
            OnPossessed(controller);
        }

        private void OnPossessed(IController controller)
        {
            Possessed?.Invoke(controller);
            PossessedEvent?.Invoke();
        }

        /// <summary>
        /// Invoked when this pawn is unpossesed by a controller
        /// </summary>
        public virtual void Unpossess()
        {
            var tempController = ActiveController;
            ActiveController = null;
            OnUnpossessed(tempController);
        }

        private void OnUnpossessed(IController controller)
        {
            Unpossessed?.Invoke(controller);
            UnpossessedEvent?.Invoke();
        }

        public virtual void Restart()
        {

        }

        /// <summary>
        /// Sets up Input component for this Pawn
        /// </summary>
        protected virtual void SetupInputComponent()
        {
            m_inputComponentReady = true;
            //if input component was defined, use it;
            if (m_StartingInputComponent != null)
            {
                InputComponent = m_StartingInputComponent;
                return;
            }

            if (InputComponent == null)
            {
                //TODO: check if component exists. 
                InputComponent = this.gameObject.AddComponent<InputComponent>();
            }
        }
    }
}

