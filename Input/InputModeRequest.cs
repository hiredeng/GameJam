using UnityEngine;

namespace Pripizden.InputSystem
{
    public abstract partial class InputProvider
    {
        public class InputModeRequest
        {
            private InputProvider m_context;

            public InputMode InputMode
            {
                get;
            }

            public GameObject lastSelectedGameObject;

            public InputModeRequest(InputMode inputMode, InputProvider context)
            {
                InputMode = inputMode;
                m_context = context;
            }

            public void Release()
            {
                if (m_context != null)
                {
                    m_context.ReleaseInputModeRequest(this);
                }
            }
        }
    }
}