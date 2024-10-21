using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;

namespace Pripizden.AudioSystem.SoundBoard
{
#if UNITY_EDITOR


    [CreateNodeName(name: "* FMOD Event")]
    [CreateNodeName(name: "FMOD/FMOD Event")]
    partial class FmodEvent
    {
        [System.Serializable]
        class EditorHelper : UnityEngine.ScriptableObject
        {
            [SerializeField]
            public Pripizden.Legacy.FmodEvent Property;
            [SerializeField]
            public string spoof = "";
        }

        UnityEditor.SerializedObject m_serializedObject = null;
        UnityEditor.SerializedProperty m_serializedProperty = null;
        EditorHelper m_editorHelper = null;
        public override bool OnNodeGUI()
        {
            //Size = new Vector2(1200, 200);
            var sze = Size;
            sze.x = 400;
            sze.y = 170;
            Size = sze;

            var groupRect = Rect;
            GUI.BeginGroup(groupRect);

            if(Application.isPlaying)
            {
                if (m_serializedObject != null) UnityEditor.Editor.DestroyImmediate(m_editorHelper);
                m_serializedObject = null;
            }
            else
            { 
                if (m_serializedObject == null)
                {
                    m_editorHelper = UnityEditor.Editor.CreateInstance<EditorHelper>();
                    m_editorHelper.Property = m_fmodEvent;
                    m_serializedObject = new UnityEditor.SerializedObject(m_editorHelper);
                    m_serializedProperty = m_serializedObject.FindProperty("Property");
                }

                if(m_serializedObject!=null)
                {
                    if (IsSelected)
                    {
                        UnityEditor.EditorGUI.BeginChangeCheck();
                        UnityEditor.EditorGUI.PropertyField(new Rect(35, 50, 370, 55), m_serializedProperty, true);
                        if (UnityEditor.EditorGUI.EndChangeCheck() || m_fmodEvent != m_editorHelper.Property)
                        {
                            m_fmodEvent = m_editorHelper.Property;
                        }
                    }
                    else
                    {
                        if (m_fmodEvent != null)
                        {
                            UnityEditor.EditorGUI.LabelField(new Rect(55, 70, 300, 25), m_fmodEvent.EventRef.Path);
                        }
                    }
                }
                GUI.BeginGroup(new Rect(45, 125, 300, 100));
                is3D = UnityEditor.EditorGUI.Toggle(new Rect(0, 0, 300, 27), "is3D", is3D);
                //isPhrase = UnityEditor.EditorGUI.Toggle(new Rect(0, 20, 300, 27), "isPhrase", isPhrase);
                GUI.EndGroup();
            }
            GUI.EndGroup();
            return false;
        }

        public override string Name { 
            get 
            {
                
                if (m_fmodEvent!=null&& !m_fmodEvent.EventRef.IsNull)
                    return m_fmodEvent.EventRef.Path;
                else
                    return m_name;
            } 
            set 
            { m_name = value; } 
        }
    }
#endif
    public partial class FmodEvent : Node, IUpdatable
    {
        [SerializeField]
        Pripizden.Legacy.FmodEvent m_fmodEvent = null;
        [SerializeField] bool is3D = false;

        [SerializeField] bool POOL = false;


        List<Pripizden.Legacy.FmodEvent> m_pool = new List<Pripizden.Legacy.FmodEvent>();

        ValueInPort<Vector3> PositionValue;
        EventOutPort m_eventStarted;

        bool m_isloaded = false;
        public override void SetupNode()
        {
            AttachEventInput(">", "ply", Play);
            AttachEventInput("=", "pse", Pause);
            AttachEventInput("x", "stp", Stop);
            AttachValueOutput<Pripizden.Legacy.FmodEvent>("fmod", "fmd", ValueProvider);
            m_eventStarted = AttachEventOutput("OnStart", "evStrt");
            AttachValueOutput<bool>("IsPlaying", "evPln", IsPlaying);
            PositionValue = AttachValueInput<Vector3>("Position", "pos");
        }
        
        Pripizden.Legacy.FmodEvent ValueProvider()
        {
            return m_fmodEvent;
        }

        bool IsPlaying()
        {
            return m_fmodEvent.Initialized && m_fmodEvent.IsPlaying();
        }

        public override void OnEnable(BoardContext context)
        {
            try
            {
                m_fmodEvent.Init();
            }
            catch
            {
                Debug.Log($"{m_fmodEvent.EventRef} is missing");
            }
            if (m_fmodEvent.Initialized)
            {
                m_isloaded = true;
                m_pool.Add(m_fmodEvent);
            }
        }

        public override void OnDisable()
        {
            if (m_isloaded)
            {
                m_isloaded = false;
                m_fmodEvent.Stop(true);
                m_fmodEvent.Release();
            }
            //m_coroutineContext.StopCoroutine(m_coroutine);
        }

        public void Update(float deltaTime)
        {

        }

        void Play()
        {
            if (!m_isloaded) return;
            if (is3D)
            {
                m_fmodEvent.Init(PositionValue.Value);
            }
            PlayImmediate();
        }

        void Pause()
        {
            if (!m_isloaded) return;
            if (m_fmodEvent.IsPaused())
            {
                m_fmodEvent.unPause();
            }
            else
            {
                m_fmodEvent.Pause();
            }
        }

        void Stop()
        {
            if (!m_isloaded) return;
            m_fmodEvent.Stop();
        }

        private void PlayImmediate()
        {
            if (!m_isloaded) return;
            if (POOL)
            {
                var yes = false;
                foreach (Pripizden.Legacy.FmodEvent f in m_pool)
                {
                    if (!f.IsPlaying())
                    {
                        f.Play();
                        m_eventStarted?.Invoke();
                        yes = true;
                        break;
                    }
                }
                if (!yes)
                {
                    var fm = new Pripizden.Legacy.FmodEvent(m_fmodEvent.EventRef);
                    m_pool.Add(fm);
                    fm.Play();
                    m_eventStarted?.Invoke();
                }
            }
            else
            {
                m_fmodEvent.Play();
                m_eventStarted?.Invoke();
            }
        }
    }


}

