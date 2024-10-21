using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pripizden.InputSystem
{
    /// <summary>
    /// Interface for objects that own input components
    /// </summary>
    public interface IInputEnabledObject
    {
        /// <summary>
        /// Input component associated with this object
        /// </summary>
        InputComponent InputComponent { get; }

        /// <summary>
        /// If should include this object in input processing
        /// </summary>
        bool bInputEnabled { get; set; }
    }
}