using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;

namespace Pripizden.AudioSystem.SoundBoard
{
    [CreateNodeName(name: "Debug/Label")]
    public class Label : Node
    {
        [SerializeField] string m_label = "";

        public override void SetupNode()
        {
#if UNITY_EDITOR
            m_guiStyle = new GUIStyle();
            m_guiStyle.fontSize = 200;
            m_guiStyle.normal.textColor = Color.white;
            m_guiStyle.alignment = TextAnchor.MiddleCenter;
#endif
        }

#if UNITY_EDITOR
        GUIStyle m_guiStyle = null;
        public override bool OnNodeGUI()
        {
            Size = new Vector2(1200f, 500f);
            
            
            GUI.BeginGroup(Rect);
            var textRect = new Rect(300, 25, 300, 50);
            var labelRect = new Rect(25, 100, 1150, 400);
            UnityEditor.EditorGUI.BeginChangeCheck();
            m_label = UnityEditor.EditorGUI.TextField(textRect, m_label);
            UnityEditor.EditorGUI.LabelField(labelRect, m_label, m_guiStyle);
            if (UnityEditor.EditorGUI.EndChangeCheck())
            {
                UnityEditor.EditorUtility.SetDirty(Board);
            }
            GUI.EndGroup();
            return false;
        }
#endif
    }
}
