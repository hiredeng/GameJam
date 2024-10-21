using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;

namespace Pripizden.AudioSystem.SoundBoard
{
    [CreateNodeName(name: "Debug/Button")]
    public partial class Button : Node
    {
        private EventOutPort m_outPort = null;
        public override void SetupNode()
        {
            m_outPort = AttachEventOutput("click", "clk");
        }
    }

#if UNITY_EDITOR
    partial class Button
    {
        public override bool OnNodeGUI()
        {
            GUI.BeginGroup(Rect);
            var newRect = new Rect(5, 30, 100, 24);
            if (GUI.Button(newRect, "Invoke"))
            {
                m_outPort?.Invoke();
            }
            GUI.EndGroup();
            return false;
        }
    }
#endif

}