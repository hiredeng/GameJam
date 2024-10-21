using FMOD;
using FMOD.Studio;
using FMODUnity;
using JetBrains.Annotations;
using ProjectName.Core;
using ProjectName.Data;
using ProjectName.Services.Configuration;
using ProjectName.Services.Logging;
using ProjectName.Services.StaticData;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ProjectName.Services.Sound
{
    public class FmodSoundService : ISoundService
    {
        private const string MainBusName = "Bus:/";
        private const string SfxBusName = "Bus:/SFX";
        private const string MusicBusName = "Bus:/MUS";

        public float MusicVolume { get => _soundConfig.Music.Volume; }
        public float SfxVolume { get => _soundConfig.Sfx.Volume; }
        public bool MusicMuted { get => _soundConfig.Music.Mute; }
        public bool SfxMuted { get => _soundConfig.Sfx.Mute; }

        public event Action Initialized;
        public event Action<bool> SfxMuteToggled;
        public event Action<bool> MusicMuteToggled;

        private bool _initBusses = false;

        FMOD.Studio.Bus _mainBus;
        FMOD.Studio.Bus _sfxBus;
        FMOD.Studio.Bus _musicBus;

        private readonly ICoroutineRunner _coroutineContext;
        private readonly ILogService _logger;
        private readonly IConfigurationService _configurationService;
        private readonly IStaticDataService _staticDataService;
        private SoundConfiguration _soundConfig;

        private Dictionary<SfxType, EventInstance> _sfxCache = new Dictionary<SfxType, EventInstance>();

        public FmodSoundService(ICoroutineRunner coroutineContext, IConfigurationService configurationService, IStaticDataService staticDataService, ILogService logger)
        {
            _coroutineContext = coroutineContext;
            _logger = logger;
            _configurationService = configurationService;
            _staticDataService = staticDataService;
        }       

        public void Init()
        {
            _logger.Log("[FmodSoundService]::Init", "Initializing");
            _soundConfig = _configurationService.Configuration.SoundConfig;
            _coroutineContext.StartCoroutine(InitRoutine());
        }

        private IEnumerator InitRoutine()
        {
            _logger.Log("[FmodSoundService]::InitRoutine", "Loading Banks");

            InitBusses();

            _mainBus.setVolume(_soundConfig.Master.Volume);
            _mainBus.setMute(_soundConfig.Master.Mute);

            _musicBus.setVolume(_soundConfig.Music.Volume);
            _musicBus.setMute(_soundConfig.Music.Mute);

            _sfxBus.setVolume(_soundConfig.Sfx.Volume);
            _sfxBus.setMute(_soundConfig.Sfx.Mute);


            string[] bankNames = new string[]
            {
                "Master",
                "Master.strings",
                "MUS",
                "SFX"
            };

            foreach (string bank in bankNames)
            {
                _logger.Log("[FmodSoundService]::InitRoutine", $"Bank load started : {bank}");
                FMODUnity.RuntimeManager.LoadBank(bank, true);
                while (!FMODUnity.RuntimeManager.HasBankLoaded(bank))
                    yield return null;
                _logger.Log("[FmodSoundService]::InitRoutine", $"Bank loaded : {bank}");
            }

            while (FMODUnity.RuntimeManager.AnySampleDataLoading())
            {
                yield return null;
            }

            Initialized?.Invoke();
        }

        private bool InitBusses()
        {
            if (!_initBusses && FMODUnity.RuntimeManager.IsInitialized)
            {
                _initBusses = true;
                _mainBus = FMODUnity.RuntimeManager.GetBus(MainBusName);
                _sfxBus = FMODUnity.RuntimeManager.GetBus(SfxBusName);
                _musicBus = FMODUnity.RuntimeManager.GetBus(MusicBusName);

                _mainBus.lockChannelGroup();

            }
            return _initBusses;
        }

        public void ToggleSfx()
        {
            _soundConfig.Sfx.SetMute(!_soundConfig.Sfx.Mute);
            if (SfxMuted) _sfxBus.setVolume(0f);
            else _sfxBus.setVolume(SfxVolume);
            OnMuteSfx(_soundConfig.Sfx.Mute);
        }
        public void SetSfxVolume(float volume)
        {
            _soundConfig.Sfx.SetVolume(volume);
            if (!InitBusses()) return; _sfxBus.setVolume(volume);
        }
        public void OnMuteSfx(bool muteState)
        {
            SfxMuteToggled?.Invoke(muteState);
        }


        public void ToggleMusic()
        {
            _soundConfig.Music.SetMute(!_soundConfig.Music.Mute);

            if (MusicMuted) _musicBus.setVolume(0f);
            else _musicBus.setVolume(MusicVolume);
            OnMuteMusic(_soundConfig.Music.Mute);
        }

        public void SetMusicVolume(float volume)
        {
            _soundConfig.Music.SetVolume(volume);
            if (!InitBusses()) return; _musicBus.setVolume(volume);
        }
        public void OnMuteMusic(bool muteState)
        {
            MusicMuteToggled?.Invoke(muteState);
        }

        public void PlaySfx(SfxType sfxType)
        {
            if (!_sfxCache.TryGetValue(sfxType, out EventInstance sfxInstance))
            {
                EventReference eventRef = _staticDataService.GetSoundEvent(sfxType);
                sfxInstance = RuntimeManager.CreateInstance(eventRef);
            }
            sfxInstance.start();
        }
    }
}