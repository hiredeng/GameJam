using System;
using UnityEngine;

namespace Pripizden.InputSystem
{
    public class AxisBinding:InputBinding
    {
        public string AxisName;
        public Action<float> AxisDelegate;

        public AxisBinding(string axisName, Action<float> axisDelegate):base()
        {
            AxisName = axisName;
            AxisDelegate = axisDelegate;
        }
    }
}