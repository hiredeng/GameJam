using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using FMOD.Studio;
using FMODUnity;

namespace Pripizden.Legacy
{
    [System.Serializable]
    public class FmodEvent
    {
        /*#if UNITY_EDITOR
                private static List<FmodEvent> __events = new List<FmodEvent>();
        #endif
        */
        [SerializeField]
        private EventReference eventRef;
        private bool released;

        private FMOD.Studio.EventInstance m_fmodEventInstance;
        public FMOD.Studio.EventInstance Instance
        {
            get { return m_fmodEventInstance; }
            private set { m_fmodEventInstance = value; }
        }
        private bool isInitialized = false;

        private Rigidbody2D rb;
        private Transform trans;

        private bool prepare_to_be_released = false;

        public bool Initialized { get { return isInitialized; } }

        //public Action<EventReference> onStoppedPlaying;
        //public Action<FMOD.Studio.EventInstance> onEventInstanceStoppedPlaying;

        public EventReference EventRef { get { return eventRef; } }

        public void setProperty(FMOD.Studio.EVENT_PROPERTY index, float value)
        {
            Instance.setProperty(index, value);
        }

        public bool IsPlaying()
        {
            FMOD.Studio.PLAYBACK_STATE state;
            Instance.getPlaybackState(out state);
            if (state != FMOD.Studio.PLAYBACK_STATE.STOPPED)
            { return true; }
            else
            {
                return false;
            }

        }

        public FmodEvent(EventReference eventRef)
        {
            this.eventRef = eventRef;
        }

        public void Init()
        {
            if (eventRef.IsNull)
            {
                Debug.LogAssertion("Sound reference not set");
                return;
            }
            Instance = FMODUnity.RuntimeManager.CreateInstance(eventRef);
            isInitialized = true;
        }

        public bool Is3D()
        {
            bool is3D;
            FMODUnity.RuntimeManager.GetEventDescription(eventRef).is3D(out is3D);
            return is3D;
        }

        public int getTime()
        {
            int a;
            Instance.getTimelinePosition(out a);
            return a;
        }
        public void setTime(int time)
        {
            Instance.setTimelinePosition(time);
        }

        public void Init(Transform trans)
        {
            if(!isInitialized) Init();
            Instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(trans));
            Rigidbody2D rb = trans.GetComponent<Rigidbody2D>();
            if (rb)
            {
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(Instance, trans, rb);
            }

            this.trans = trans;
            this.rb = rb;
        }

        public void Init(Transform trans, Rigidbody2D rb)
        {
            if (!isInitialized) Init();
            Instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(trans));

            FMODUnity.RuntimeManager.AttachInstanceToGameObject(Instance, trans, rb);
            this.trans = trans;
            this.rb = rb;
        }

        public void Init(Vector3 pos)
        {
            if (!isInitialized) Init();
            Instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(pos));
        }

        public void PlayWithNoStopping()
        {
            CheckInit();

            FMOD.Studio.PLAYBACK_STATE state;
            Instance.getPlaybackState(out state);
            if (state == FMOD.Studio.PLAYBACK_STATE.STOPPED)
            {
                Play();
            }
        }

        public void CheckPosition()
        {
            if (trans != null) Instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(trans));
            else return;
            if (rb != null) FMODUnity.RuntimeManager.AttachInstanceToGameObject(Instance, trans, rb);
            else return;
        }

        public void Play()
        {
            if (!CheckInit()) return;
            if (prepare_to_be_released) return;
            CheckPosition();
            Instance.start();
        }

        public bool IsPaused()
        {
            CheckInit();
            var b = false;
            Instance.getPaused(out b);
            return b;
        }
        public void Pause()
        {
            CheckInit();
            Instance.setPaused(true);
        }

        public void unPause()
        {
            CheckInit();
            Instance.setPaused(false);
        }

        public void Stop(bool nofade = false)
        {
            if (CheckInit())
            {
                Instance.stop((nofade ? FMOD.Studio.STOP_MODE.IMMEDIATE : FMOD.Studio.STOP_MODE.ALLOWFADEOUT));
            }
        }

        public void SetParameter(string param, float val)
        {
            CheckInit();
            /*Debug.Log(Instance);
            Debug.Log(Instance.hasHandle());
            Debug.Log(Instance.handle);
            Debug.Log(Instance.isValid());
            Debug.Log(param);
            Debug.Log(val);
            Debug.Log(Instance.setParameterByName(param, val));*/
            Instance.setParameterByName(param, val);
        }

        public float GetParameter(string param)
        {
            CheckInit();
            float v;
            Instance.getParameterByName(param, out v);
            return v;
        }

        public void SetPitch(float pitch)
        {
            CheckInit();
            Instance.setPitch(pitch);
        }

        public void SetVolume(float volume)
        {
            CheckInit();
            Instance.setVolume(volume);
        }

        public void Release()
        {
            CheckInit();
            Instance.setCallback(null);
            Instance.release();
            isInitialized = false;
/*#if UNITY_EDITOR
            __events.Remove(this);
            Debug.Log("FMODEVENTS: " + __events.Count.ToString());
#endif*/
        }

        public void TriggerCue()
        {
            throw new System.InvalidOperationException();
            //Instance.triggerCue();
        }

        private bool CheckInit()
        {
            if (!isInitialized) Debug.LogError("Not initialized FmodEvent " + eventRef);
            return isInitialized;
        }

        public static string EventToToken(string ev)
        {
            return ev.Remove(0, ev.LastIndexOf("/") + 1);
        }

        public static string TokenToEvent(string Token)
        {
            var t = "event:/Dialogue/";

            var p1 = Token.IndexOf("_");
            var p2 = Token.IndexOf("_", p1 + 1);

            if (p1 != -1 && p2 != 1)
            {
                t += Token.Substring(p1 + 1, p2 - p1 - 1) + "/";
                t += Token.Substring(0, p1) + "/";
                t += Token;

                return t;
            }
            return "";
        }

        public static string SeparateToken(string Subtitle)
        {
            var p1 = Subtitle.IndexOf("%");
            var p2 = Subtitle.IndexOf("%", p1 + 1);
            if (p1 != -1 && p2 != 1)
            {
                var q = Subtitle.Substring(p1 + 1, p2 - p1 - 1);
                return q;
            }
            return "";
        }

    }
}
