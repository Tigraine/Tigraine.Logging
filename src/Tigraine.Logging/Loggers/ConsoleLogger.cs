namespace Tigraine.Logging.Loggers
{
    using System;

    public class ConsoleLogger : AbstractLogger
    {
        public ConsoleLogger(LogLevel logLevel) : base(logLevel)
        {
        }

        protected override void WriteDebug(string message)
        {
            Console.WriteLine("[DEBUG] {0}", message);
        }

        protected override void WriteInformation(string message)
        {
            Console.WriteLine("[INFO] {0}", message);
        }

        protected override void WriteWarning(string message)
        {
            Console.WriteLine("[WARN] {0}", message);
        }

        protected override void WriteError(string message)
        {
            Console.WriteLine("[ERRO] {0}", message);
        }
    }
}