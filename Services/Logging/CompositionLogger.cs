namespace ProjectName.Services.Logging
{
    public class CompositionLogger : ILogService
    {
        private readonly ILogService[] _loggers;

        public CompositionLogger(ILogService[] loggers) =>
            _loggers = loggers;

        public void Log(string tag, object message)
        {
            foreach(ILogService logger in _loggers)
            {
                logger.Log(tag, message);
            }
        }
    }
}