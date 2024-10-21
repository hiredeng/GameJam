using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;

namespace Pripizden.AudioSystem.SoundBoard
{
    public class ConvertNode : Node
    {
        private ValueInPort<int> m_intIn;
        public override void SetupNode()
        {
            m_intIn = AttachValueInput<int>("int", "intVal");
            AttachValueOutput<float>("float", "floatVal", ValueProvider);
        }

        public float ValueProvider()
        {
            return (float) m_intIn.Value;
        }


        
    }
}