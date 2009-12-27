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