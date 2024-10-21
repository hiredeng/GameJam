using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pripizden.InputSystem
{
    public enum ActionEvent
    {
        /// <summary>
        /// When button is initially pressed down
        /// </summary>
        Pressed,

        /// <summary>
        /// When button is released
        /// </summary>
        Released,

        /// <summary>
        /// If two clicks occur in quick succession. 
        /// </summary>
        DoubleClick,

        /// <summary>
        /// Each frame the button is held
        /// </summary>
        Held,
    }

}