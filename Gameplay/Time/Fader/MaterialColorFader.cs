using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace ProjectName.Gameplay.Timing
{
    public class MaterialColorFader : BaseTimeObserver
    {
        [SerializeField]
        Material m_material;

        [SerializeField]
        private string m_parameterName;
        [SerializeField]
        private Gradient m_gradient;

        public override void NotifyTime(float time)
        {
            m_material.SetColor(m_parameterName, m_gradient.Evaluate(time));
        }

        public void FetchColor()
        {
            if (m_material != null)
            {
                var color = m_material.GetColor(m_parameterName);
                var colorkeys = m_gradient.colorKeys;

                for (int i = 0; i < colorkeys.Length; i++)
                {
                    var ck = colorkeys[i];
                    ck.color = color;
                    colorkeys[i] = ck;
                }
                m_gradient.colorKeys = colorkeys;
            }
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(MaterialColorFader))]
    public class MaterialColorFaderInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            

            if (GUILayout.Button("Fetch"))
            {
                var mcf = (MaterialColorFader)serializedObject.targetObject;
                if (mcf != null)
                    mcf.FetchColor();
            }
        }
    }

#endif
}