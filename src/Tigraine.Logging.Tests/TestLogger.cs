namespace Tigraine.Logging.Tests
{
    using System.Collections.Generic;

    public class TestLogger : AbstractLogger
    {
        readonly IDictionary<LogLevel, IList<string>> logEvents = new Dictionary<LogLevel, IList<string>>();

        public IList<string> GetLogEvents(LogLevel level)
        {
            return logEvents[level];
        }

        public TestLogger(LogLevel logLevel) : base(logLevel)
        {
            logEvents.Add(new KeyValuePair<LogLevel, IList<string>>(LogLevel.Debug, new List<string>()));
            logEvents.Add(new KeyValuePair<LogLevel, IList<string>>(LogLevel.Information, new List<string>()));
            logEvents.Add(new KeyValuePair<LogLevel, IList<string>>(LogLevel.Warning, new List<string>()));
            logEvents.Add(new KeyValuePair<LogLevel, IList<string>>(LogLevel.Error, new List<string>()));
        }

        protected override void WriteDebug(string message)
        {
            logEvents[LogLevel.Debug].Add(message);
        }

        protected override void WriteInformation(string message)
        {
            logEvents[LogLevel.Information].Add(message);
        }

        protected override void WriteWarning(string message)
        {
            logEvents[LogLevel.Warning].Add(message);
        }

        protected override void WriteError(string message)
        {
            logEvents[LogLevel.Error].Add(message);
        }
    }
}