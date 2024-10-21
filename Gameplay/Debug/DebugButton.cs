using UnityEngine;
using UnityEngine.Events;

namespace ProjectName.Gameplay.Debug
{
    public class DebugButton : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _action;

        public void Invoke()
            => _action?.Invoke();
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(DebugButton))]
    public class DebugButtonInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var targ = (DebugButton)target;
            if (GUILayout.Button("Invoke"))
                targ.Invoke();
        }
    }
#endif
}
