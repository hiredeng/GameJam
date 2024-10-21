using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pripizden.Gameplay.Character
{
    /// <summary>
    /// Base class for pawn controllers
    /// </summary>
    public abstract class Controller : MonoBehaviour, IController
    {
        /// <summary>
        /// Currently possessed pawn
        /// </summary>
        public IPawn ActivePawn
        {
            get;
            protected set;
        } = null;

        public event Action<IPawn> Possessed;

        public event Action<IPawn> Unpossessed;

        protected virtual void OnDestroy()
        {
            if (ActivePawn != null)
            {
                ActivePawn.Unpossess();
            }
        }

        /// <summary>
        /// possesses specified pawn;
        /// </summary>
        /// <param name="newPawn">pawn to posses</param>
        public virtual void Possess(IPawn newPawn)
        {
            // Proceed only when we have a new pawn, and it's not the same as we're currently possessing
            if (newPawn == null || newPawn == ActivePawn)
                return;

            if (ActivePawn != null)
            {
                Unpossess();
            }

            //Unpossess pawn from controller, if it is currently possessed;
            if (newPawn.ActiveController != null)
            {
                newPawn.ActiveController.Unpossess();
            }

            ActivePawn = newPawn;

            newPawn.PossessBy(this);
            Possessed?.Invoke(newPawn);
        }

        /// <summary>
        /// unpossesses specified pawn;
        /// </summary>
        public virtual void Unpossess()
        {
            if (ActivePawn != null)
            {
                ActivePawn.Unpossess();
                Unpossessed?.Invoke(ActivePawn);
            }

            ActivePawn = null;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Controller), true)]
    public class ControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (EditorApplication.isPlaying)
            {
                var controller = target as Controller;
                var helpString = "";

                if (controller.ActivePawn == null)
                {
                    helpString = "Not possessed to any pawn";
                }
                else
                {
                    helpString = $"Posessed"; // to {controller.ActivePawn.gameObject.name}
                }
                EditorGUILayout.HelpBox(helpString, MessageType.Info, true);
            }

            DrawDefaultInspector();
        }
    }
#endif
}