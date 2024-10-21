using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pripizden.Gameplay.Parameter
{
    public class ParameterContainer : MonoBehaviour
    {
        Dictionary<string, ParameterValue> m_parameterDatabase = new Dictionary<string, ParameterValue>();

        public event Action Restarted;

        public void Restart()
        {
            m_parameterDatabase.Clear();
        }

        public void NotifySubscribers()
        {
            Restarted?.Invoke(); 
        }

        public void AddParameter(string name, float initialValue)
        {
            if(m_parameterDatabase.ContainsKey(name))
            {
                Debug.Log($"[Stats] setting {name} to {initialValue}");
                m_parameterDatabase[name].Value = initialValue;
            }
            else
            {
                Debug.Log($"[Stats] Creating {name} with {initialValue}");
                var param = new ParameterValue();
                param.Value = initialValue;
                m_parameterDatabase[name] = param; 
            }
        }
        public bool HasParameter(string name)
        {
            return m_parameterDatabase.ContainsKey(name);
        }

        public ParameterValue GetParameter(string name)
        {
            if(m_parameterDatabase.TryGetValue(name, out ParameterValue val))
                return val;
            return null;
        }
    }
}