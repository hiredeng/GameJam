using Pripizden.Gameplay;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace ProjectName.Gameplay.Timing
{

    [RequireComponent(typeof(Light2D))]
    public class LightColorFader : BaseTimeObserver
    {
        [SerializeField]
        private AnimationCurve m_intensity;
        [SerializeField]
        private Gradient m_gradient;

        private Light2D m_light;

        private void Awake()
        {
            m_light = GetComponent<Light2D>();
        }

        public override void NotifyTime(float time)
        {
            m_light.color = m_gradient.Evaluate(time);
            m_light.intensity = m_intensity.Evaluate(time);
        }

#if UNITY_EDITOR
        public void EditorEvaluate(float time)
        {
            var light = GetComponent<Light2D>();
            if(light!=null)
            {
                light.color = m_gradient.Evaluate(time);
                light.intensity = m_intensity.Evaluate(time);
                EditorUtility.SetDirty(this.gameObject);
            }
        }
#endif
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(LightColorFader))]
    public class LightColorFaderInspector : Editor
    {
        float timeValue = 0f;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            
            timeValue = GUILayout.HorizontalSlider(timeValue, 0f, 1f);
            GUILayout.Space(32f);

            if (GUILayout.Button("Evaluate"))
            {
                ((LightColorFader)serializedObject.targetObject).EditorEvaluate(timeValue);
            }
        }
    }

#endif
}