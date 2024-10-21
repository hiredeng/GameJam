using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectName.Gameplay.Timing
{
    public class TimeView : BaseTimeObserver
    {
        [SerializeField]
        Image _image;


        [SerializeField]
        float _treashold = 0.76f;
        bool once = false;

        [SerializeField]
        UnityEvent _invoke;

        public override void NotifyTime(float time)
        {
            if((time > _treashold) && !once )
            {
                once = true;
                _invoke?.Invoke();
            }
            if (time < _treashold) once = false;

            _image.fillAmount = time;
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(TimeView))]
    public class TimeViewInspector : Editor
    {
        float timeValue = 0f;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();


            timeValue = GUILayout.HorizontalSlider(timeValue, 0f, 1f);
            GUILayout.Space(32f);

            if (GUILayout.Button("Evaluate"))
            {
                ((TimeView)serializedObject.targetObject).NotifyTime(timeValue);
            }
        }
    }

#endif
}