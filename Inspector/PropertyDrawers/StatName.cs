using Pripizden.Gameplay.Parameter;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace ProjectName.Inspector
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(StatNameAttribute))]
    public class StatNameDrawer : PropertyDrawer
    {
        SerializedProperty _property;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            UnityEditor.EditorGUI.BeginProperty(position, label, property);
            _property = property;
            var currentOption = property.stringValue;
            label.text = $"{label.text}: {currentOption}";
            if (!EditorGUI.DropdownButton(position, label, FocusType.Passive))
            {
                return;
            }
            var constants = GetConstants(typeof(Parameters));

            GenericMenu menu = new GenericMenu();
            foreach(var field in constants)
            {              
                    menu.AddItem(new GUIContent(field.Name), false, OnItemClicked, field.Name);
            }
            menu.DropDown(position);
            UnityEditor.EditorGUI.EndProperty();
        }

        private List<FieldInfo> GetConstants(System.Type type)
        {
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public |
                 BindingFlags.Static | BindingFlags.FlattenHierarchy);

            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
        }

        void OnItemClicked(object parameter)
        {
            _property.stringValue = (string)parameter;
            _property.serializedObject.ApplyModifiedProperties();
        }
    }
#endif

    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
    public class StatNameAttribute : PropertyAttribute
    {
        public StatNameAttribute()
        {

        }
    }
}