/*
 * Copyright 2009 Daniel Hölbling - http://www.tigraine.at
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *  http://www.apache.org/licenses/LICENSE-2.0
 *  
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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