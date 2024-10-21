using UnityEngine;

namespace ProjectName.UI
{
    public abstract class AbstractBarkUI : MonoBehaviour, IBarkUI
    {

        public abstract bool isPlaying { get; }

        public abstract void Bark(Subtitle subtitle);

        public abstract void Hide();

    }
}
