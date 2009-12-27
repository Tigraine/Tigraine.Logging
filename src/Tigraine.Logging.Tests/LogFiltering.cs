namespace Tigraine.Logging.Tests
{
    using Xunit;

    public class LogFiltering
    {
        [Fact]
        public void WillNotCallDebugIfLevelIsHigher()
        {
            var consoleLogger = new TestLogger(LogLevel.None);
            consoleLogger.Debug("Test");

            Assert.Empty(consoleLogger.GetLogEvents(LogLevel.Debug));
        }

        [Fact]
        public void WillNotCallInformationIfLevelIsHigher()
        {
            var consoleLogger = new TestLogger(LogLevel.None);
            consoleLogger.Information("Test");

            Assert.Empty(consoleLogger.GetLogEvents(LogLevel.Information));
        }

        [Fact]
        public void WillNotCallWarningIfLevelIsHigher()
        {
            var consoleLogger = new TestLogger(LogLevel.None);
            consoleLogger.Warning("Test");

            Assert.Empty(consoleLogger.GetLogEvents(LogLevel.Warning));
        }

        [Fact]
        public void WillNotCallErrorIfLevelIsHigher()
        {
            var consoleLogger = new TestLogger(LogLevel.None);
            consoleLogger.Error("Test");

            Assert.Empty(consoleLogger.GetLogEvents(LogLevel.Error));
        }

        [Fact]
        public void CallsErrorIfLevelIsError()
        {
            var consoleLogger = new TestLogger(LogLevel.Error);
            consoleLogger.Error("Test");

            Assert.NotEmpty(consoleLogger.GetLogEvents(LogLevel.Error));
        }
    }
}