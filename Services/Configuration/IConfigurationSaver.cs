using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectName.Services.Configuration
{
    public interface IConfigurationSaver
    {
        void Load();
        void Save();
    }
}