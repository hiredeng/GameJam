using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;

namespace Pripizden.AudioSystem.SoundBoard
{
    [CreateNodeName(name: "Logic/Gate")]
    public class Gate : Node
    {

        EventOutPort m_output = null;
        ValueInPort<bool> m_condition = null;
        public override void SetupNode()
        {

            m_output = AttachEventOutput("", "evout");
            AttachEventInput("", "evin", EventHandler);
            m_condition = AttachValueInput<bool>("Condition", "condin");
        }

        void EventHandler()
        {
            if(m_condition.Value)
            {
                m_output.Invoke();
            }
        }


#if UNITY_EDITOR
        public override bool OnNodeGUI()
        {
            Size = new Vector2(80, Size.y);
            return false;
        }
#endif

    }
}