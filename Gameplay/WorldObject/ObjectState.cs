
using UnityEngine;
using UnityEngine.Events;

namespace ProjectName.Gameplay.WorldObject
{
    public class ObjectState : MonoBehaviour
    {
        public string Name;

        [SerializeField]
        private UnityEvent Entered;
        [SerializeField]
        private UnityEvent Exited;

        private void Reset()
        {
            Name = gameObject.name;
        }

        public virtual void Enter()
        {
            Entered?.Invoke();
        }
        public virtual void Exit()
        {
            Exited?.Invoke();
        }
    }
}