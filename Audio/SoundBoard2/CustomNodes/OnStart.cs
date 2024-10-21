using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;

namespace Pripizden.AudioSystem.SoundBoard
{
    [CreateNodeName("* Start")]
    public partial class OnStart : Node
    {

        EventOutPort m_port = null;
        public override void SetupNode()
        {
            m_port = AttachEventOutput("OnStart", "strt");
        }

        public override void Start()
        {
            m_port.Invoke();
        }
    }

#if UNITY_EDITOR
    partial class OnStart
    {
        
    }
#endif

}