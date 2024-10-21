using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;

namespace Pripizden.AudioSystem.SoundBoard
{
    [CreateNodeName(name: "Logic/Tick")]
    public class Tick : Node, IUpdatable
    {

        private EventOutPort m_outPort = null;
        public override void SetupNode()
        {
            m_outPort = AttachEventOutput(">", "o");
        }

        public override void OnEnable(BoardContext context)
        {
            context.RegisterUpdatable(this);
        }

        public void Update(float delta)
        {
            m_outPort.Invoke();
        }

#if UNITY_EDITOR
        public override bool OnNodeGUI()
        {
            Size = new Vector2(40, 60);
            return false;
        }
#endif

    }
}