namespace Tigraine.Logging.Tests
{
    using Loggers;
    using ObjectRenderers;
    using Xunit;

    public class CompositeRendererTests
    {
        [Fact]
        public void WillUseBaseClassIfCurrentClassNotFound()
        {
            var compositeRenderer = new CompositeRenderer<TestClass>(p => p.Firstname + " " + p.Nickname + " " + p.Age);
            var testClass = new TestClass() {Firstname = "Daniel", Nickname = "Tigraine", Age = 24};
            
            string render = compositeRenderer.Render(testClass);

            Assert.Equal("Daniel Tigraine 24", render);
        }

        internal class TestClass
        {
            public string Firstname { get; set; }
            public string Nickname { get; set; }
            public int Age { get; set; }
        }
    }
}