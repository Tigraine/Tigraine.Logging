namespace Tigraine.Logging.Tests
{
    using Loggers;
    using ObjectRenderers;
    using Xunit;

    public class RecursiveObjectRendererLookup
    {
        public void WillUseBaseClassIfCurrentClassNotFound()
        {
            var compositeRenderer = new CompositeRenderer<TestClass>(p => p.Hello + " " + p.World + " " + p.Age);
            var consoleLogger = new ConsoleLogger(LogLevel.Debug);
            consoleLogger.AddObjectRenderer(typeof(TestClass), compositeRenderer);
        }

        internal class TestClass
        {
            public string Hello { get; set; }
            public string World { get; set; }
            public int Age { get; set; }
        }
    }
}