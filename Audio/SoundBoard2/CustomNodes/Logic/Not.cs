using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;

namespace Pripizden.AudioSystem.SoundBoard
{
    [CreateNodeName(name: "Logic/Not")]
    public class Not : Node
    {

        ValueInPort<bool> m_value = null;
        public override void SetupNode()
        {
            m_value = AttachValueInput<bool>("A", "condin");
            AttachValueOutput<bool>("!A", "condout", ValueHandler);
        }

        bool ValueHandler()
        {
            return !m_value.Value;
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