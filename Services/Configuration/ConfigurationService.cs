using ProjectName.Data;

namespace ProjectName.Services.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        public AppConfiguration Configuration { get; set; }
    }
}