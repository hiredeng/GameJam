using Zeropoint.BoardSystem;

namespace Pripizden.AudioSystem
{

    public partial class SoundRuntime : BoardContext
    {
        protected override void Awake()
        {
            base.Awake();
        }

        private void OnDisable()
        {
            for (int i = m_soundBoards.Count - 1; i >= 0; i--)
            {
                m_soundBoards[i].UnloadBoard();
            }
        }
    }
}