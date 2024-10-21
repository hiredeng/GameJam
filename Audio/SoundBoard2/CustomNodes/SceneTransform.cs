using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;

namespace Pripizden.AudioSystem.SoundBoard
{
    [CreateNodeName(name: "Level/Scene Transform")]
    public partial class SceneTransform : Node
    {
        
        [SerializeField]
        string m_guid = "";

        PropertyName m_propertyName;
        Vector3 m_lastKnownPosition;
        Transform m_transform;
        BoardContext m_context;

        public override void SetupNode()
        {
            AttachValueOutput<Vector3>("Position", "pos", Vector3Provider);
            AttachValueOutput<Transform>("Transform", "tfm", TransformProvider);
        }
        public override void OnEnable(BoardContext context)
        {
            m_context = context;
        }

        public override void OnDisable()
        {
            
        }

        bool ValidateTransform()
        {
            if (m_transform == null)
            {
                m_propertyName = new PropertyName(m_guid);
                var obj = m_context.GetReferenceValue(m_propertyName, out bool valid);
                if(valid)
                {
                    m_transform = (Transform)obj;
                    m_lastKnownPosition = m_transform.position;
                    return true;
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        Vector3 Vector3Provider()
        {
            if (ValidateTransform())
            {
                m_lastKnownPosition = m_transform.position;
            }
            return m_lastKnownPosition;
        }

        Transform TransformProvider()
        {
            ValidateTransform();
            return m_transform;
        }

    }

#if UNITY_EDITOR

    partial class SceneTransform
    {
        Transform m_editorTransform = null;
        public override bool OnNodeGUI()
        {

            Size = new Vector2(225, Size.y);

            if(!string.IsNullOrEmpty(m_guid)&&m_editorTransform==null)
            {
                m_propertyName = new PropertyName(m_guid);
                var controller = GameObject.FindObjectOfType<SoundBoardController>();
                if (controller != null)
                {
                    var table = controller as IExposedPropertyTable;
                    var foundTransform = table.GetReferenceValue(m_propertyName, out bool valid);
                    if(valid)
                    {
                        m_editorTransform = (Transform)foundTransform;
                    }
                }
            }

            GUI.BeginGroup(Rect);
            Rect newRect = new Rect(5, 30, 150, 29);
            UnityEditor.EditorGUI.BeginChangeCheck();
            var newTransform = UnityEditor.EditorGUI.ObjectField(newRect, m_editorTransform, typeof(Transform), true);
            if(UnityEditor.EditorGUI.EndChangeCheck())
            {
                var controller = GameObject.FindObjectOfType<SoundBoardController>();
                if (controller != null)
                {
                    var table = controller as IExposedPropertyTable;
                    m_editorTransform = (Transform)newTransform;
                    if(string.IsNullOrEmpty(m_guid))
                    {
                        m_guid = System.Guid.NewGuid().ToString();
                        m_propertyName = new PropertyName(m_guid);
                    }
                    table.SetReferenceValue(m_propertyName, newTransform);
                    UnityEditor.EditorUtility.SetDirty(Board);
                    UnityEditor.EditorUtility.SetDirty(controller);
                }
            }
            GUI.EndGroup();


            return false;
        }
    }
#endif
}