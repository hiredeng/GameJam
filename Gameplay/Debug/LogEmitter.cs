using UnityEngine;

namespace ProjectName.Gameplay.Debug
{
    public class LogEmitter : MonoBehaviour
    {
        [SerializeField]
        private string Message;

        [ContextMenu("Emit Default")]
        public void Emit()
            => UnityEngine.Debug.Log($"{gameObject.name} > {Message}");

        public void Emit(string msg)
            => UnityEngine.Debug.Log($"{gameObject.name} > {msg}");
    }
}