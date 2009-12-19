namespace Tigraine.Logging.Tests
{
    using System;

    public class TestLogger : AbstractLogger
    {
        public TestLogger(LogLevel logLevel) : base(logLevel)
        {
        }

        protected override void WriteDebug(string message)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInformation(string message)
        {
            throw new NotImplementedException();
        }

        protected override void WriteWarning(string message)
        {
            throw new NotImplementedException();
        }

        protected override void WriteError(string message)
        {
            throw new NotImplementedException();
        }
    }
}