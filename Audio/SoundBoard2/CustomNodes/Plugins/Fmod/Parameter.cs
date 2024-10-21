using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;

namespace Pripizden.AudioSystem.SoundBoard
{
    [CreateNodeName(name: "FMOD/Parameter-Move")]
    public partial class ParameterMove : Node
    {
        [SerializeField]
        string m_Parameter = "";

        [SerializeField]
        float m_endValue = 1f;
        [SerializeField]
        float m_transitionTime = 1f;

        ValueInPort<Pripizden.Legacy.FmodEvent> m_fmodPort;

        BoardContext m_context;
        public override void SetupNode()
        {
            m_fmodPort = AttachValueInput<Pripizden.Legacy.FmodEvent>("fmod", "fmIn");
            AttachValueOutput<Pripizden.Legacy.FmodEvent>("fmod", "fmOut", () => { return m_fmodPort.Value; });

            AttachEventInput("Go", "prSt", Do);
            AttachValueOutput("Value", "prVlOut", GetParameter);
        }

        public override void OnEnable(BoardContext context)
        {
            m_context = context;
        }

        public void Do()
        {
            var fmodEvent = m_fmodPort.Value;
            if (fmodEvent != null && fmodEvent.Initialized)
            {
                m_context.StartCoroutine(RotateAB(fmodEvent, m_Parameter, fmodEvent.GetParameter(m_Parameter), m_endValue, m_transitionTime));
            }
        }

        IEnumerator RotateAB(Pripizden.Legacy.FmodEvent ev, string parameter, float A, float B, float time)
        {
            var delta = ((B - A) / time);
            while ((delta > 0) && (A < B) || (delta < 0) && (A > B))
            {
                A += delta * Time.deltaTime;
                ev.SetParameter(parameter, A);
                yield return null;
            }
            ev.SetParameter(parameter, B);
            yield break;
        }

        float GetParameter()
        {
            var fmodEvent = m_fmodPort.Value;
            if (fmodEvent != null && fmodEvent.Initialized)
            {
                return fmodEvent.GetParameter(m_Parameter);
            }

            return 0f;
        }
    }

#if UNITY_EDITOR
    partial class ParameterMove
    {
        public override bool OnNodeGUI()
        {
            var sze = Size;
            sze.y = 170;
            Size = sze;
            GUI.BeginGroup(Rect);
            UnityEditor.EditorGUI.BeginChangeCheck();


            UnityEditor.EditorGUI.LabelField(new Rect(10, 80, 95, 24), "Parameter");
            UnityEditor.EditorGUI.LabelField(new Rect(10, 110, 95, 24), "To");
            UnityEditor.EditorGUI.LabelField(new Rect(10, 140, 95, 24), "Time");

            m_Parameter = UnityEditor.EditorGUI.TextField(new Rect(60, 80, 95, 24), m_Parameter);           
            m_endValue = UnityEditor.EditorGUI.FloatField(new Rect(60, 110, 95, 24), m_endValue);
            m_transitionTime = UnityEditor.EditorGUI.FloatField(new Rect(60, 140, 95, 24), m_transitionTime);


            if (UnityEditor.EditorGUI.EndChangeCheck())
            {
                UnityEditor.EditorUtility.SetDirty(Board);
            }
            GUI.EndGroup();
            return false;
        }
    }
#endif



    [CreateNodeName(name: "FMOD/Parameter-A->B")]
    public partial class ParameterAtoB : Node
    {
        [SerializeField]
        string m_Parameter = "";

        [SerializeField]
        float m_startValue = 0f;
        [SerializeField]
        float m_endValue = 1f;
        [SerializeField]
        float m_transitionTime = 1f;

        ValueInPort<Pripizden.Legacy.FmodEvent> m_fmodPort;

        BoardContext m_context;
        public override void SetupNode()
        {
            m_fmodPort = AttachValueInput<Pripizden.Legacy.FmodEvent>("fmod", "fmIn");
            AttachValueOutput<Pripizden.Legacy.FmodEvent>("fmod", "fmOut", () => { return m_fmodPort.Value; });

            AttachEventInput("Go", "prSt", Do);
            AttachValueOutput("Value", "prVlOut", GetParameter);
        }

        public override void OnEnable(BoardContext context)
        {
            m_context = context;
        }

        public void Do()
        {
            var fmodEvent = m_fmodPort.Value;
            if (fmodEvent != null && fmodEvent.Initialized)
            {
                m_context.StartCoroutine(RotateAB(fmodEvent, m_Parameter, m_startValue, m_endValue, m_transitionTime));
            }
        }

        IEnumerator RotateAB(Pripizden.Legacy.FmodEvent ev,string parameter, float A, float B, float time)
        {
            var delta = ((B-A) / time);
            while((delta>0)&&(A<B)||(delta<0)&&(A>B))
            {
                A += delta * Time.deltaTime;
                ev.SetParameter(parameter, A);
                yield return null;
            }
            ev.SetParameter(parameter, B);
            yield break;
        }

        float GetParameter()
        {
            var fmodEvent = m_fmodPort.Value;
            if (fmodEvent != null && fmodEvent.Initialized)
            {
                return fmodEvent.GetParameter(m_Parameter);
            }

            return 0f;
        }
    }

#if UNITY_EDITOR
    partial class ParameterAtoB
    {
        public override bool OnNodeGUI()
        {
            var sze = Size;
            sze.y = 200;
            Size = sze;
            GUI.BeginGroup(Rect);
            UnityEditor.EditorGUI.BeginChangeCheck();


            UnityEditor.EditorGUI.LabelField(new Rect(10, 80, 95, 24), "Parameter");
            UnityEditor.EditorGUI.LabelField(new Rect(10, 110, 95, 24), "From");
            UnityEditor.EditorGUI.LabelField(new Rect(10, 140, 95, 24), "To");
            UnityEditor.EditorGUI.LabelField(new Rect(10, 170, 95, 24), "Time");

            m_Parameter = UnityEditor.EditorGUI.TextField(new Rect(60, 80, 95, 24), m_Parameter);
            m_startValue = UnityEditor.EditorGUI.FloatField(new Rect(60, 110, 95, 24), m_startValue);
            m_endValue = UnityEditor.EditorGUI.FloatField(new Rect(60, 140, 95, 24), m_endValue);
            m_transitionTime = UnityEditor.EditorGUI.FloatField(new Rect(60, 170, 95, 24), m_transitionTime);


            if (UnityEditor.EditorGUI.EndChangeCheck())
            {
                UnityEditor.EditorUtility.SetDirty(Board);
            }
            GUI.EndGroup();
            return false;
        }
    }
#endif



    [CreateNodeName(name: "FMOD/Parameter-Set")]
    public partial class Parameter : Node
    {
        [SerializeField]
        string m_Parameter = "";

        ValueInPort<Pripizden.Legacy.FmodEvent> m_fmodPort;
        ValueInPort<float> m_valuePort;
        public override void SetupNode()
        {
            m_fmodPort = AttachValueInput<Pripizden.Legacy.FmodEvent>("fmod", "fmIn");
            AttachValueOutput<Pripizden.Legacy.FmodEvent>("fmod1", "fmOut", ()=> { return m_fmodPort.Value; });

            AttachEventInput("Set", "prSt", SetParameter);
            AttachValueOutput("Value", "prVlOut", GetParameter);
            
            m_valuePort = AttachValueInput<float>("Value", "prVl");
        }

        float GetParameter()
        {
            var fmodEvent = m_fmodPort.Value;
            if (fmodEvent != null && fmodEvent.Initialized)
            {
                return fmodEvent.GetParameter(m_Parameter);
            }

            return 0f;
        }

        void SetParameter()
        {
            var fmodEvent = m_fmodPort.Value;
            if(fmodEvent!=null && fmodEvent.Initialized)
            {
                fmodEvent.SetParameter(m_Parameter, m_valuePort.Value);
            }
        }
    }

#if UNITY_EDITOR
    partial class Parameter
    {
        public override bool OnNodeGUI()
        {            
            GUI.BeginGroup(Rect);
            var newRect = new Rect(60, 80, 95, 24);
            UnityEditor.EditorGUI.BeginChangeCheck();
            m_Parameter = UnityEditor.EditorGUI.TextField(newRect, m_Parameter);
            if (UnityEditor.EditorGUI.EndChangeCheck())
            {
                UnityEditor.EditorUtility.SetDirty(Board);
            }
            GUI.EndGroup();
            return false;
        }
    }
#endif

}