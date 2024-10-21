using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;

namespace Pripizden.AudioSystem.SoundBoard
{
    [CreateNodeName(name: "Constant/Float")]
    public partial class Constant : Node
    {
        [SerializeField]
        float m_value = 0f;
        public override void SetupNode()
        {
            AttachValueOutput<float>("value", "val", ValueProvider);
        }

        float ValueProvider()
        {
            return m_value;
        }
    }

#if UNITY_EDITOR
    partial class Constant
    {
        public override bool OnNodeGUI()
        {
            GUI.BeginGroup(Rect);
            var newRect = new Rect(5, 30, 70, 24);
            UnityEditor.EditorGUI.BeginChangeCheck();
            m_value = UnityEditor.EditorGUI.FloatField(newRect, m_value);
            if(UnityEditor.EditorGUI.EndChangeCheck())
            {
                UnityEditor.EditorUtility.SetDirty(Board);
            }
            GUI.EndGroup();
            return false;
        }
    }
#endif

}