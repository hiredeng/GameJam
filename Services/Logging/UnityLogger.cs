namespace ProjectName.Services.Logging
{
    public class UnityLogger : ILogService
    {
        UnityEngine.ILogger _logger;
        public UnityLogger()
        {
            _logger = UnityEngine.Debug.unityLogger;
        }

        public void Log(string tag, object message)
        {
            _logger.Log(tag, message);
        }
    }
}