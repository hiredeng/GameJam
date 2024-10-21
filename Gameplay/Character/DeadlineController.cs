using Pripizden.InputSystem;
using UnityEngine;
using ProjectName.UI.Services;
using VContainer;

namespace Pripizden.Gameplay.Character
{
    public class DeadlineController : PlayerController
    {
        private IUIFactory _uiFactory;

        [Inject]
        public void Construct(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        protected override void SetupInputComponent()
        {
            base.SetupInputComponent();

            InputComponent.BindAction("Pause", ActionEvent.Pressed, HandlePause);
        }
        
        private void HandlePause()
        {
            if (Time.timeScale > 0f && ActivePawn!=null)
            {
                _uiFactory.CreatePauseWindow();
            }
        }
    }
}