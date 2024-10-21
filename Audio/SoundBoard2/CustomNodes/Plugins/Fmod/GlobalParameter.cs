using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;

namespace Pripizden.AudioSystem.SoundBoard
{

    [CreateNodeName(name: "FMOD/Global-Parameter-Set")]
    public partial class GlobalParameter : Node
    {
        [SerializeField]
        string m_Parameter = "";

        ValueInPort<float> m_valuePort;
        public override void SetupNode()
        {
            AttachEventInput("Set", "prSt", SetParameter);
            AttachValueOutput("Value", "prVlOut", GetParameter);
            
            m_valuePort = AttachValueInput<float>("Value", "prVl");
        }

        float GetParameter()
        {
            var result = FMODUnity.RuntimeManager.StudioSystem.getParameterByName(m_Parameter, out float oval);
            if (result == FMOD.RESULT.OK)
            {
                return oval;
            }
            return 0f;
        }

        void SetParameter()
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(m_Parameter, m_valuePort.Value);
        }
    }

#if UNITY_EDITOR
    partial class GlobalParameter
    {
        public override bool OnNodeGUI()
        {            
            GUI.BeginGroup(Rect);
            var newRect = new Rect(60, 60, 95, 24);
            UnityEditor.EditorGUI.BeginChangeCheck();
            m_Parameter = UnityEditor.EditorGUI.TextField(newRect, m_Parameter);
            if (UnityEditor.EditorGUI.EndChangeCheck())
            {
                UnityEditor.EditorUtility.SetDirty(Board);
            }
            GUI.EndGroup();
            return false;
        }
    }
#endif

}