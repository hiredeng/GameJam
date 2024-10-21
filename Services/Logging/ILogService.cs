using ProjectName.Core.ServiceModel;

namespace ProjectName.Services.Logging
{
    public interface ILogService : IService
    {
        public void Log(string tag, object message);
    }
}