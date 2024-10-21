using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pripizden.DataValues
{
    public abstract class AbstractObjectsGroup : ScriptableObject
    {
#if UNITY_EDITOR
        public virtual Type GetElementType()
        {
            return null;
        }

        public virtual IEnumerator GetEnumerator()
        {
            return null;
        }

        public virtual int GetElementsCount()
        {
            return 0;
        }
#endif
    }

    public abstract class BaseObjectsGroup<T> : AbstractObjectsGroup
    {
        private HashSet<T> m_objects = new HashSet<T>();

        public IReadOnlyCollection<T> Objects
        {
            get => m_objects;
        }

        public void AddObject(T obj)
        {
            m_objects.Add(obj);
        }

        public void RemoveObject(T obj)
        {
            m_objects.Remove(obj);
        }

#if UNITY_EDITOR
        public override Type GetElementType()
        {
            return typeof(T);
        }

        public override IEnumerator GetEnumerator()
        {
            return m_objects.GetEnumerator();
        }

        public override int GetElementsCount()
        {
            return m_objects.Count;
        }
#endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(AbstractObjectsGroup), true)]
    public class AbstractObjectsGroupEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(Application.isPlaying)
            {
                AbstractObjectsGroup objectsGroup = (AbstractObjectsGroup)target;
                IEnumerator objectsIenumerator = objectsGroup.GetEnumerator();

                GUILayout.Label($"Objects in group ({objectsGroup.GetElementsCount()}):");

                EditorGUI.BeginDisabledGroup(true);
                while (objectsIenumerator.MoveNext())
                {
                    if(objectsIenumerator.Current == null)
                    {
                        GUILayout.Label("None");
                        continue;
                    }

                    if (objectsIenumerator.Current is UnityEngine.Object)
                    {
                        EditorGUILayout.ObjectField(objectsIenumerator.Current as UnityEngine.Object, objectsGroup.GetElementType(), true);
                    }
                    else
                    {
                        GUILayout.Label(objectsIenumerator.Current.ToString());
                    }
                }
                EditorGUI.EndDisabledGroup();
            }
        }
    }
#endif
}