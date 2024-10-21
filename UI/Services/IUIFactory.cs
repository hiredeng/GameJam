using Pripizden.UI;
using ProjectName.Core.ServiceModel;
using TempCharacter = Pripizden.Gameplay.Character.Character;

namespace ProjectName.UI.Services
{
    public interface IUIFactory : IService
    {
        void CreateUIRoot();
        MainMenuPanel CreateMainMenu();
        void CreateHudRoot();
        void OpenWindow();
        void CreatePauseWindow();

        CharacterHud CreateHUD(TempCharacter forCharacter);
    }
}