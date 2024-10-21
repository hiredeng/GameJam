using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;
using Pripizden.DataValues;

namespace Pripizden.AudioSystem.SoundBoard
{
    [CreateNodeName(name: "")]
    public abstract partial class ValueNode<T, ST> : Node, DataValues.IEventListener<ST> where T : DataValues.BaseEvent<ST>
    {
        [SerializeField]
        SerializedAssetReference<T> m_valueEventReference = new SerializedAssetReference<T>();

        EventOutPort m_trigPort = null;
        ST m_lastValue = default(ST);
        public override void SetupNode()
        {
            m_trigPort = AttachEventOutput("Received", "trig");
            AttachValueOutput<ST>("Value", "val", ValueProvider);
        }
        public override void OnEnable(BoardContext context)
        {
            if (m_valueEventReference.HasAsset)
            {
                m_valueEventReference.Asset.AddListener(this);
            }
        }

        public override void OnDisable()
        {
            if (m_valueEventReference.HasAsset)
            {
                m_valueEventReference.Asset.RemoveListener(this);
            }
        }

        public void Invoke(ST arg)
        {
            m_lastValue = arg;
            m_trigPort.Invoke();
        }

        public ST ValueProvider()
        {
            return m_lastValue;
        }

        public override SerializedAssetReference[] GetSerializedAssets()
        {
            return new SerializedAssetReference[] { m_valueEventReference };
        }
    }

    [CreateNodeName(name: "Value/IntEvent")]
    public partial class IntValue : ValueNode<DataValues.IntEvent, int>
    {
        
    }

    [CreateNodeName(name: "Value/FloatEvent")]
    public partial class FloatValue : ValueNode<DataValues.FloatEvent, float>
    {

    }

#if UNITY_EDITOR

    partial class ValueNode<T, ST>
    {
        T m_valueEvent = null;
        public override bool OnNodeGUI()
        {
            bool outputValue = false;
            GUI.BeginGroup(Rect);
            var newRect = new Rect(5, 30, 70, 48);
            UnityEditor.EditorGUI.BeginChangeCheck();
            try
            {
                if (m_valueEvent == null && m_valueEventReference.HasAsset) m_valueEvent = m_valueEventReference.Asset;
                m_valueEvent = (T)UnityEditor.EditorGUI.ObjectField (newRect, m_valueEvent, typeof(T), false);
            }
            catch
            {
                Debug.Log("<color=green>SoundEventReceiver: you most probably opened the filthy circle menu no one should ever touch</color>");
            }
            if (UnityEditor.EditorGUI.EndChangeCheck())
            {
                outputValue = true;
                m_valueEventReference.Asset = m_valueEvent;
                if (Board != null)
                {
                    UnityEditor.EditorUtility.SetDirty(Board);
                }
            }
            GUI.EndGroup();
            return outputValue;
        }
    }
#endif
}