using UnityEngine;
using Pripizden.Service.UI;
using ProjectName.Core.ServiceModel;
using ProjectName.World;
using ProjectName.Services.Sound;
using FMODUnity;

namespace ProjectName.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        Transform GetUiRoot();
        WindowData GetWindow(WindowId windowId);

        LevelData GetLevel(int level);
        WorldSegment GetRoomData(string guid);

        EventReference GetSoundEvent(SfxType sfxType);
    }
}
