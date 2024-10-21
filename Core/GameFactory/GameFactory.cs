using Pripizden.Gameplay.Character;
using ProjectName.Core.AssetManagement;
using ProjectName.Extensions;
using ProjectName.Gameplay.Mission;
using ProjectName.Services.StaticData;
using ProjectName.World;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ProjectName.Core.Factory
{
    public class GameFactory : IGameFactory
    {
        const string CharacterPath = "Characters/PlayerCharacter";
        const string CharacterControllerPath = "Characters/Controller";
        const string WorldWrapperPath = "World/WorldWrapper";

        private readonly IWorldBuilder _worldBuilder;
        private readonly IObjectResolver _objectResolver;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        public GameFactory(IWorldBuilder worldBuilder, IObjectResolver objectResolver,  IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _worldBuilder = worldBuilder;
            _objectResolver = objectResolver;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public DeadlineController CreateCharacterController() =>
            _assetProvider.Instantiate<DeadlineController>(CharacterControllerPath);

        public Character CreateCharacter(Vector3 where) =>
            _assetProvider.Instantiate<Character>(CharacterPath, where);

        public WorldData CreateWorld()
        {
            LevelData level = _staticDataService.GetLevel(0);
            level.SegmentGuids.ForEach(x => _worldBuilder.WithSegment(_staticDataService.GetRoomData(x)));
            WorldData worldData = _worldBuilder.Build();
            return worldData;
        }

        public MissionCoordinator CreateMission()
        {
            return null;
        }

        public WorldWrapper CreateWorldWrapper(WorldData worldData, Transform target) => 
            _assetProvider.Instantiate<WorldWrapper>(WorldWrapperPath)
                .With(x => x.Initialize(worldData, target));

    }
}