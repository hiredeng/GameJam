using UnityEngine;

namespace Pripizden
{
    [CreateAssetMenu(fileName = "new SoundEvent", menuName = "SoundEvent", order = 50)]
    public class SoundEvent : ScriptableObject
    {
        public void Invoke()
        {
            Invoke(Vector3.zero);
        }

        public void Invoke(Transform body)
        {
            Legacy.Dispatcher.Instance.SendSfx(base.name, body);
        }

        public void Invoke(Vector3 position)
        {
            Legacy.Dispatcher.Instance.SendSfx(base.name, position);
        }
    }



#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(SoundEvent))]
    public class SoundEventPropertyDrawer : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Invoke"))
            {
                var soundEvent = (SoundEvent)target;
                soundEvent?.Invoke();
            }
        }
    }
#endif
}
