using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ProjectName.Data
{
    [System.Serializable]
    public class AppConfiguration
    {
        public SoundConfiguration SoundConfig;
        public LocalizationConfiguration LanguageConfig;
        public InterfaceConfiguration InterfaceConfig;
        public ScreenConfiguration ScreenConfig;

        public void InitializeDefaults()
        {
            SoundConfig = new SoundConfiguration();
            LanguageConfig = new LocalizationConfiguration();
            InterfaceConfig = new InterfaceConfiguration();
            ScreenConfig = new ScreenConfiguration();
        }
    }
}