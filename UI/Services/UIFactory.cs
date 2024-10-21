using Pripizden.Gameplay.Character;
using Pripizden.Service.UI;
using Pripizden.UI;
using ProjectName.Core.AssetManagement;
using ProjectName.Services.StaticData;
using UnityEngine;


namespace ProjectName.UI.Services
{
    public class UIFactory : IUIFactory
    {
        


        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private Transform _uiRoot;
        private Transform _hudRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public void CreateUIRoot() => 
            _uiRoot = _assetProvider.Instantiate(NamedAssetPath.UIRootPath).transform;

        public void CreateHudRoot() =>
            _hudRoot = _assetProvider.Instantiate(NamedAssetPath.HudRootPath).transform;

        public MainMenuPanel CreateMainMenu()
        {
            return _assetProvider.InstantiateWithParent<MainMenuPanel>(NamedAssetPath.MainMenuPath, _uiRoot);
        }
        public void OpenWindow()
        {
            
        }

        public void CreatePauseWindow()
        {
            WindowData config = _staticDataService.GetWindow(WindowId.PauseWindow);

            PausePanel window = _assetProvider.InstantiateWithParent(config.Template, _uiRoot) as PausePanel;
            window.Construct();
        }

        public CharacterHud CreateHUD(Character forCharacter)
        {
            var hud = _assetProvider.InstantiateWithParent<CharacterHud>(NamedAssetPath.HudPath, _hudRoot);
            hud.Init(forCharacter);
            return hud;
        }
    }
}