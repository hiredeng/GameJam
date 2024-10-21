using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;

#if UNITY_EDITOR
//using UnityEditor;
#endif

namespace Pripizden.AudioSystem.SoundBoard
{
    public class SoundBoardController : MonoBehaviour, IExposedPropertyTable
    {
        [SerializeField]
        Board m_soundBoard;

        [SerializeField] //[HideInInspector]
        List<PropertyName> m_propertyNames;
        [SerializeField] //[HideInInspector]
        List<UnityEngine.Object> m_propretyReferences;

        public Board SoundBoard { get { return m_soundBoard; } }

        private void Start()
        {
            var inst = BoardContext.Instance;
            if(inst!=null)
            {
                inst.RegisterTable(this);
                inst.RegisterBoard(m_soundBoard);
            }
        }

        private void OnDisable()
        {
            var inst = BoardContext.Instance;
            if (inst != null)
            {
                inst.UnregisterTable(this);
            }
        }

        public void ClearReferenceValue(PropertyName id)
        {
            int index = m_propertyNames.IndexOf(id);
            if (index != -1)
            {
                m_propretyReferences.RemoveAt(index);
                m_propertyNames.RemoveAt(index);
            }
        }

        public Object GetReferenceValue(PropertyName id, out bool idValid)
        {
            int index = m_propertyNames.IndexOf(id);
            if (index != -1)
            {
                idValid = true;
                return m_propretyReferences[index];
            }
            idValid = false;
            return null;
        }
        public void SetReferenceValue(PropertyName id, Object value)
        {
            int index = m_propertyNames.IndexOf(id);
            if (index != -1)
            {
                m_propretyReferences[index] = value;
            }
            else
            {
                m_propertyNames.Add(id);
                m_propretyReferences.Add(value);
            }
        }
    }
}