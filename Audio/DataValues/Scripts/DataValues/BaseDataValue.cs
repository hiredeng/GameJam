using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pripizden.DataValues
{
    public abstract class AbstractDataValue : ScriptableObject 
    {
        public virtual void Revert() { }

#if UNITY_EDITOR
        public virtual void OnEditorValueChangedTrigger() { }

        public virtual void SetRuntimeAsInitial() { }
#endif
    }

    public abstract class BaseDataValue<T> : AbstractDataValue
    {
        public delegate void ValueChangedDelegate(BaseDataValue<T> dataValue);

        public event ValueChangedDelegate OnValueChanged;

        [SerializeField]
        private T m_value;

        private T m_runtimeValue;

        public T Value
        {
            get => m_runtimeValue;
            set
            {
                if (m_runtimeValue.Equals(value))
                    return;

                m_runtimeValue = value;

                OnValueChanged?.Invoke(this);
            }
        }

        public T InitialValue
        {
            get => m_value;
        }

        private void OnEnable()
        {
            m_runtimeValue = m_value;
        }

        public override void Revert()
        {
            Value = InitialValue;
        }

        public override string ToString()
        {
            return m_runtimeValue.ToString();
        }

#if UNITY_EDITOR
        public override void OnEditorValueChangedTrigger()
        {
            OnValueChanged?.Invoke(this);
        }

        public override void SetRuntimeAsInitial()
        {
            m_value = m_runtimeValue;
        }
#endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(AbstractDataValue), true)]
    public class AbstractDataValueEditor : Editor
    {
        private SerializedProperty m_runtimeProperty;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            AbstractDataValue dataValue = ((AbstractDataValue)target);


            if(Application.isPlaying)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label("Runtime", GUILayout.Width(EditorGUIUtility.labelWidth));

                switch (dataValue)
                {
                    case BoolDataValue b:
                        b.Value = GUILayout.Toggle(b.Value, GUIContent.none);
                        break;
                    case FloatDataValue f:
                        f.Value = EditorGUILayout.FloatField(f.Value);
                        break;
                    case IntDataValue i:
                        i.Value = EditorGUILayout.IntField(i.Value);
                        break;
                    case StringDataValue s:
                        s.Value = EditorGUILayout.TextField(s.Value);
                        break;
                }

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                if(GUILayout.Button("Revert to initial"))
                {
                    dataValue.Revert();
                }

                if(GUILayout.Button("Set from runtime value"))
                {
                    dataValue.SetRuntimeAsInitial();
                }

                GUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Invoke OnValueChanged"))
            {
                dataValue.OnEditorValueChangedTrigger();
            }
        }
    }
#endif

    //TODO: Custom editor
}