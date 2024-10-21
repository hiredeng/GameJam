using System;
using UnityEngine;

namespace Pripizden.InputSystem
{
    public class Vector2Binding : InputBinding
    {
        public string Vector2Name;
        public Action<Vector2> Vector2Delegate;

        public Vector2Binding(string vector2Name, Action<Vector2> vector2Delegate):base()
        {
            Vector2Name = vector2Name;
            Vector2Delegate = vector2Delegate;
        }
    }
}
