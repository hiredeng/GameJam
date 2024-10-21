using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;

namespace Pripizden.AudioSystem.SoundBoard
{
    [CreateNodeName(name: "Debug/Log Float")]
    public class DebugLoggerFloat : Node
    {
        ValueInPort<float> m_value;

        public override void SetupNode()
        {
            AttachEventInput("Log", "log", LogHandler);
            m_value = AttachValueInput<float>("value", "val");
        }

        void LogHandler()
        {
            Debug.Log(m_value.Value);
        }
    }

    [CreateNodeName(name: "Debug/Log Bool")]
    public class DebugLoggerBool : Node
    {
        ValueInPort<bool> m_value;

        public override void SetupNode()
        {
            AttachEventInput("Log", "log", LogHandler);
            m_value = AttachValueInput<bool>("value", "val");
        }

        void LogHandler()
        {
            Debug.Log(m_value.Value);
        }
    }

    [CreateNodeName(name: "Debug/Log Vector3(Postion)")]
    public class DebugLoggerVector3 : Node
    {
        ValueInPort<Vector3> m_value;

        public override void SetupNode()
        {
            AttachEventInput("Log", "log", LogHandler);
            m_value = AttachValueInput<Vector3>("value", "val");
        }

        void LogHandler()
        {
            Debug.Log(m_value.Value);
        }
    }

    [CreateNodeName(name: "Debug/Log String")]
    public partial class DebugLoggerString : Node
    {
        [SerializeField]
        string m_string = "Ooops";

        public override void SetupNode()
        {
            AttachEventInput("Log", "log", LogHandler);
        }

        void LogHandler()
        {
            Debug.Log(m_string);
        }
    }


#if UNITY_EDITOR

    partial class DebugLoggerString
    {
        public override bool OnNodeGUI()
        {
            GUI.BeginGroup(Rect);
            var newRect = new Rect(50, 33, 100, 24);
            UnityEditor.EditorGUI.BeginChangeCheck();
            m_string = UnityEditor.EditorGUI.TextField(newRect, m_string);
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
