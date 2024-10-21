using Pripizden.InputSystem;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Pripizden.Gameplay.Character 
{
    public class TutorialPawn : Pawn
    {
        [SerializeField]
        List<GameObject> _firstClicks = null;

        [SerializeField]
        DeadlineController _controller = null;

        [SerializeField]
        Character _character = null;

        [SerializeField]
        UnityEvent OnDone;

        int clicks = 0;

        bool once = false;
        protected override void SetupInputComponent()
        {
            base.SetupInputComponent();

            InputComponent.BindAction("MouseClick", ActionEvent.Pressed, HandleMouse);
        }

        public override void PossessBy(IController controller)
        {
            base.PossessBy(controller);

            _firstClicks[0].SetActive(true);
            once = true;
        }



        void HandleMouse()
        {
            _firstClicks[clicks].SetActive(false);
            clicks++;
            if (clicks < _firstClicks.Count)
            {
                _firstClicks[clicks].SetActive(true);
            }
            if(clicks >= _firstClicks.Count && once)
            {
                once = false;
                OnDone?.Invoke();
                _controller.Possess(_character);
            }
        }
    }

}
