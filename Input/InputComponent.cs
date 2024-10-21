using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Pripizden.InputSystem
{
    /// <summary>
    /// InputComponent class, handles bindings for input system
    /// </summary>
    public class InputComponent : MonoBehaviour
    {
        // binding lists; 
        public List<ActionBinding> ActionBindings = new List<ActionBinding>();
        public List<AxisBinding> AxisBindings = new List<AxisBinding>();
        public List<Vector2Binding> Vector2Bindings = new List<Vector2Binding>();

        public bool bInputEnabled = true;


        /// <summary>
        /// Binds a delegate function to an input action
        /// </summary>
        /// <param name="actionName">Name of the action</param>
        /// <param name="inputEvent">Type of the event to react to</param>
        /// <param name="actionDelegate">Delegate to call when appropriate event occures</param>
        /// <returns>the created ActionBinging</returns>
        public ActionBinding BindAction(string actionName, ActionEvent inputEvent, Action actionDelegate)
        {
            //return null if receiving empty arguments
            if (String.IsNullOrEmpty(actionName) || actionDelegate == null) return null;
            var actionBinding = new ActionBinding(actionName, inputEvent, actionDelegate);
            ActionBindings.Add(actionBinding);
            return actionBinding;
        }

        /// <summary>
        /// Binds a delegate function to an input axis
        /// </summary>
        /// <param name="axisName">Name of the axis</param>
        /// <param name="axisDelegate">Axis value changed delegate</param>
        /// <returns>the created AxisBinding</returns>
        public AxisBinding BindAxis(string axisName, Action<float> axisDelegate)
        {
            //return null if receiving empty arguments
            if (String.IsNullOrEmpty(axisName) || axisDelegate == null) return null;
            var axisBinding = new AxisBinding(axisName, axisDelegate);
            AxisBindings.Add(axisBinding);
            return axisBinding;
        }
        /// <summary>
        /// Binds a delegate function to an input vector;
        /// </summary>
        /// <param name="vectorName"></param>
        /// <param name="vectorDelegate"></param>
        /// <returns></returns>
        public Vector2Binding BindVector2(string vectorName, Action<Vector2> vectorDelegate)
        {
            //return null if receiving empty arguments
            if (String.IsNullOrEmpty(vectorName) || vectorDelegate == null) return null;
            var vectorBinding = new Vector2Binding(vectorName, vectorDelegate);
            Vector2Bindings.Add(vectorBinding);
            return vectorBinding;
        }

        /// <summary>
        /// Clears all input bindings
        /// </summary>
        public void ClearActionBindings()
        {
            ActionBindings.Clear();
            AxisBindings.Clear();
            Vector2Bindings.Clear();
        }
    }
}