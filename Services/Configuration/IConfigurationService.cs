using ProjectName.Core.ServiceModel;
using ProjectName.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectName.Services.Configuration
{
    public interface IConfigurationService : IService
    {
        AppConfiguration Configuration { get; set; }
    }
}