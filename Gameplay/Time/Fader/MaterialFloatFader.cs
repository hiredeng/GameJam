using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace ProjectName.Gameplay.Timing
{

    public class MaterialFloatFader : BaseTimeObserver
    {
        [SerializeField]
        Material m_material;

        [SerializeField]
        private string m_parameterName;
        [SerializeField]
        private AnimationCurve m_curve;

        public override void NotifyTime(float time)
        {
            m_material.SetFloat(m_parameterName, m_curve.Evaluate(time));
        }

        public void FetchValue()
        {
            if (m_material != null)
            {
                var val = m_material.GetFloat(m_parameterName);
                var curveKeys = m_curve.keys;

                for (int i = 0; i < curveKeys.Length; i++)
                {
                    var ck = curveKeys[i];
                    ck.value = val;
                    curveKeys[i] = ck;
                }
                m_curve.keys = curveKeys;
            }
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(MaterialFloatFader))]
    public class MaterialFloatFaderInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Fetch"))
            {
                var mff = (MaterialFloatFader)serializedObject.targetObject;
                if (mff != null)
                    mff.FetchValue();
            }
        }
    }

#endif
}