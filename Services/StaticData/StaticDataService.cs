using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Pripizden.Service.UI;
using ProjectName.World;
using FMODUnity;
using ProjectName.Services.Sound;

namespace ProjectName.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string UiRootPath = "Static Data/UI/UiRoot";
        private const string WindowStaticDataPath = "Static Data/UI/WindowStaticData";
        private const string  WorldStaticDataPath = "Static Data/WorldStaticData";
        private const string  LevelStaticDataPath = "Static Data/LevelStaticData";
        private const string  SoundStaticDataPath = "Static Data/SoundStaticData";
        private Dictionary<WindowId, WindowData> _windowConfigs;
        private Dictionary<int, LevelData> _levelConfigs;
        private Dictionary<string, WorldSegment> _worldConfigs;
        private Dictionary<SfxType, EventReference> _soundConfigs;
        private Transform _UiRoot;

        public void Load()
        {
            _windowConfigs = Resources
                .Load<WindowStaticData>(WindowStaticDataPath)
                .Data
                .ToDictionary(x => x.WindowId, x => x);

            _worldConfigs = Resources
                .Load<WorldStaticData>(WorldStaticDataPath)
                .Data
                .ToDictionary(x => x.GUID, x => x);

            _levelConfigs = Resources
                .Load<LevelStaticData>(LevelStaticDataPath)
                .Data
                .ToDictionary(x => x.LevelID, x => x);

            _soundConfigs = Resources
                .Load<SoundStaticData>(SoundStaticDataPath)
                .Data
                .ToDictionary(x => x.Sfx, x => x.Sound);

            _UiRoot = Resources.Load<Transform>(UiRootPath);
        }

        public Transform GetUiRoot()
        {
            return _UiRoot;
        }

        public WindowData GetWindow(WindowId windowId)
        {
            return _windowConfigs[windowId];
        }

        public LevelData GetLevel(int level)
        {
            return _levelConfigs[level];
        }

        public WorldSegment GetRoomData(string guid)
        {
            return _worldConfigs[guid];
        }

        public EventReference GetSoundEvent(SfxType sfxType)
        {
            return _soundConfigs[sfxType];
        }
    }
}
