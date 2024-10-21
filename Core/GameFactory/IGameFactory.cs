using Pripizden.Gameplay.Character;
using ProjectName.Core.ServiceModel;
using ProjectName.World;
using UnityEngine;

namespace ProjectName.Core.Factory
{
    public interface IGameFactory : IService
    {
        Character CreateCharacter(Vector3 where);
        DeadlineController CreateCharacterController();
        WorldData CreateWorld();
        WorldWrapper CreateWorldWrapper(WorldData worldData, Transform target);
    }
}