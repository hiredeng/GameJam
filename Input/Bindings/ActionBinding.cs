using System;
using UnityEngine;

namespace Pripizden.InputSystem
{
    public class ActionBinding:InputBinding
    {
        public string ActionName;
        public ActionEvent ActionType;
        public Action ActionDelegate;

        public ActionBinding(string actionName, ActionEvent actionType, Action actionDelegate):base()
        {
            ActionName = actionName;
            ActionType = actionType;
            ActionDelegate = actionDelegate;
        }
    }
}