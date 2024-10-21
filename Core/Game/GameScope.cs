using ProjectName.Core.AssetManagement;
using ProjectName.Core.Factory;
using ProjectName.Core.Game;
using ProjectName.Core.Game.States;
using ProjectName.Core.StateMachine;
using ProjectName.Services.Distraction;
using ProjectName.Services.Configuration;
using ProjectName.Services.Logging;
using ProjectName.Services.Screen;
using ProjectName.Services.Sound;
using ProjectName.Services.StaticData;
using ProjectName.Services.Timing;
using ProjectName.UI.Services;
using ProjectName.World;
using System;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ProjectName.Core
{
    public class GameScope : LifetimeScope, ICoroutineRunner
    {
        [SerializeField]
        private ViewBlocker _viewBlocker;

        protected override void Configure(IContainerBuilder builder)
        {
            DontDestroyOnLoad(gameObject);

            RegisterGameFlow(builder);

            RegisterServices(builder);

            builder.RegisterScoped<IGameFactory, Factory.GameFactory>();

            builder.RegisterScoped<IWorldBuilder, WorldBuilder>();

            builder.RegisterScoped<ITimeKeeper, TimeKeeper>().As<ITickable>();

            builder.RegisterTransient<WorldWrapper>();

            builder.RegisterScoped<IDistractionService, DistractionService>();

            RegisterGameStates(builder);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.RegisterTransient<ILogService, UnityLogger>();

            builder.RegisterTransient<IAssetProvider, AssetProvider>();
            builder.RegisterSingleton<IStaticDataService, StaticDataService>();

            builder.RegisterSingleton<IConfigurationService, ConfigurationService>();
            builder.RegisterSingleton<IConfigurationSaver, ConfigurationSaver>();

            builder.RegisterSingleton<IUIFactory, UIFactory>();

            builder.RegisterSingleton<IScreenService, ScreenService>();
            builder.RegisterSingleton<ISoundService, FmodSoundService>();

        }

        private void RegisterGameFlow(IContainerBuilder builder)
        {
            ViewBlocker blocker = Instantiate(_viewBlocker, transform);
            builder.RegisterEntryPoint<Game.Game>(Lifetime.Singleton);
            builder.RegisterInstance<ViewBlocker>(blocker);
            builder.RegisterSingleton<IStateMachine, GameStateMachine>();

            builder.RegisterInstance<ICoroutineRunner>(transform.AddComponent<CoroutineRunner>());
            builder.Register<SceneLoader>(Lifetime.Singleton);
        }

        private void RegisterGameStates(IContainerBuilder builder)
        {
            builder.RegisterTransient<DataMigrationState>();
            builder.RegisterTransient<GameLoopState>();
            
            builder.RegisterTransient<LoadAppState>();
            builder.RegisterTransient<UnloadAppState>();

            builder.RegisterTransient<LoadPlayerProgressState>();
            builder.RegisterTransient<LoadLevelState>();
            builder.RegisterTransient<UnloadLevelState>();
            builder.RegisterTransient<MainMenuState>();
            
        }
    }
}