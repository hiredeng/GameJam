using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;

namespace Pripizden.AudioSystem.SoundBoard
{
    [CreateNodeName(name: "* Sound Event Receiver")]
    public partial class SoundEventReceiver : Node
    {
        [SerializeField]
        SerializedAssetReference<SoundEvent> m_soundEventAsset = new SerializedAssetReference<SoundEvent>();

        EventOutPort m_trigPort = null;
        string m_eventName = "";
        Vector3 m_lastMessagePosition;

        public override void SetupNode()
        {
            
            m_trigPort = AttachEventOutput("Received", "trig");
            AttachValueOutput<Vector3>("Position", "pos", ValueProvider);
        }
        public override void OnEnable(BoardContext context)
        {
            if(m_soundEventAsset.HasAsset)
            {
                m_eventName = m_soundEventAsset.Asset.name;
                Pripizden.Legacy.Dispatcher.Instance.Register(Legacy.MessageID.SFX, HandleMessage);
            }
        }

        public override void OnDisable()
        {
            Pripizden.Legacy.Dispatcher.Instance.Unregister(Legacy.MessageID.SFX, HandleMessage);
        }


        void HandleMessage(object o)
        {
            var q = (Pripizden.Legacy.SfxReport)o;
            if (q != null && q.Message == m_eventName)
            {
                if (q.Args != null)
                {
                    m_lastMessagePosition = q.Args.Position;
                }
                m_trigPort.Invoke();
            }
        }

        Vector3 ValueProvider()
        {
            return m_lastMessagePosition;
        }

        public override SerializedAssetReference[] GetSerializedAssets()
        {
            return new SerializedAssetReference[] { m_soundEventAsset };
        }

    }

#if UNITY_EDITOR
    
    partial class SoundEventReceiver
    {
        public override string Name { get { return m_soundEvent != null ? m_soundEvent.name : m_name; } set { m_name = value; } }
        SoundEvent m_soundEvent = null;
        public override bool OnNodeGUI()
        {
            bool outputValue = false;
            GUI.BeginGroup(Rect);
            var newRect = new Rect(5, 30, 70, 48);
            UnityEditor.EditorGUI.BeginChangeCheck();
            try
            {
                if (m_soundEvent == null && m_soundEventAsset.HasAsset) m_soundEvent = m_soundEventAsset.Asset;
                m_soundEvent = (SoundEvent)UnityEditor.EditorGUI.ObjectField(newRect, m_soundEvent, typeof(SoundEvent), false);
            }
            catch
            {
                Debug.Log("<color=green>SoundEventReceiver: you most probably opened the filthy circle menu no one should ever touch</color>");
            }
            if(UnityEditor.EditorGUI.EndChangeCheck())
            {
                outputValue = true;
                m_soundEventAsset.Asset = m_soundEvent;
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