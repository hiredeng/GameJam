using UnityEngine;

namespace ProjectName.Gameplay.Interactive.Types
{
    public class InteractionPoint : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public InteractionDirection Direction = InteractionDirection.Up;
    }

    public enum InteractionDirection
    {
        None,
        Up,
        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft,
        Left,
        UpLeft,
    }
}