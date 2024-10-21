using Newtonsoft.Json;
using ProjectName.Core.Game;
using ProjectName.Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ProjectName.Services.Configuration
{
    public class ConfigurationSaver : IConfigurationSaver
    {
        private const string ConfigurationFileName = "config.json";

        private readonly IConfigurationService _configurationService;

        public ConfigurationSaver(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public void Save()
        {
            string profilePath = Path.Combine(Application.persistentDataPath, ConfigurationFileName);
            string json = JsonConvert.SerializeObject(_configurationService.Configuration);
            File.WriteAllText(profilePath, json); 
        }

        public void Load()
        {
            string profilePath = Path.Combine(Application.persistentDataPath, ConfigurationFileName);
            if (!File.Exists(profilePath))
            {
                var tempConfig = new AppConfiguration();
                tempConfig.InitializeDefaults();
                File.WriteAllText(profilePath, JsonConvert.SerializeObject(tempConfig));
            }
            string file = File.ReadAllText(profilePath);
            AppConfiguration config = JsonConvert.DeserializeObject<AppConfiguration>(file);
            _configurationService.Configuration = config;
        }
    }
}