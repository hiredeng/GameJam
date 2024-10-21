#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Pripizden.AudioSystem
{
    public class DispatcherConsole : EditorWindow
    {
        static DispatcherConsole _current;
        [MenuItem("Pripizden/DispatcherConsole")]
        static void Initialize()
        {
            if (_current == null) _current = GetWindow<DispatcherConsole>(false, "DispatcherConsole");
        }

        private string msg;
        private LinkedList<string> onGoing = new LinkedList<string>();

        void OnEnable()
        {
            _current = this;

            EditorApplication.playModeStateChanged -= PlayModeChanged;
            EditorApplication.playModeStateChanged += PlayModeChanged;

            SceneView.duringSceneGui -= SceneGui;
            SceneView.duringSceneGui += SceneGui;
        }

        void OnDisable()
        {
            _current = null;
        }

        private void SceneGui(SceneView view)
        {
            Handles.BeginGUI();

            Handles.EndGUI();
        }

        private void PlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                Legacy.Dispatcher.Instance.Register(Legacy.MessageID.Gameplay, Receive);
            }
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                Legacy.Dispatcher.Instance.Unregister(Legacy.MessageID.Gameplay, Receive);
            }
        }

        public void Receive(object o)
        {
            var q = o as string;
            if (q != null)
            {
                addOngoing(q);
                this.Repaint();
            }
        }
        private void addOngoing(string msg)
        {
            onGoing.AddFirst(msg);
            while (onGoing.Count > 24) onGoing.RemoveLast();
        }


        private void OnGUI()
        {
            var buttonRect = Rect.MinMaxRect(10, 5, 160, 25 );
            var canvasRect = Rect.MinMaxRect(5, 30 + 20, position.width - 5, position.height - 5);
            GUI.Box(canvasRect, string.Empty);
            var stringRect = Rect.MinMaxRect(10, 35+20, position.width - 10, 55+20);

            
            var en = onGoing.GetEnumerator();
            while (en.MoveNext())
            {
                GUI.TextField(stringRect, en.Current);
                stringRect.position -= Vector2.down * 23;
            }
            


            msg = GUI.TextField(buttonRect, msg);
            buttonRect.position -= Vector2.down * 23;
            if (GUI.Button(buttonRect, "Send"))
            {
                Pripizden.Legacy.Dispatcher.Instance.Send(Legacy.MessageID.Gameplay, msg);
            }
            


        }



    }
}
#endif
