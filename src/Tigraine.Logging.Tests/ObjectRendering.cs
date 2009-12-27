namespace Tigraine.Logging.Tests
{
    using System.Linq;
    using Rhino.Mocks;
    using Xunit;

    public class ObjectRendering
    {
        [Fact]
        public void UsesDefaultObjectRendererForUnknownTypes()
        {
            var testLogger = new TestLogger(LogLevel.Debug);
            
            testLogger.Debug("Test {0}", "Test");

            Assert.Equal("Test Test", testLogger.GetLogEvents(LogLevel.Debug).First());
        }

        [Fact]
        public void UsesSpecifiedObjectRendererForType()
        {
            var testLogger = new TestLogger(LogLevel.Debug);
            var mockRenderer = MockRepository.GenerateMock<IObjectRenderer>();
            testLogger.AddObjectRenderer<string>(mockRenderer);

            testLogger.Debug("Test {0}", "Test");

            mockRenderer.AssertWasCalled(p => p.Render("Test"));
        }

        [Fact]
        public void UsesRendererOfBaseClassIfPossible()
        {
            var testLogger = new TestLogger(LogLevel.Debug);
            var mockRenderer = MockRepository.GenerateMock<IObjectRenderer>();
            testLogger.AddObjectRenderer<ObjectRendering>(mockRenderer);

            var testChildClass = new TestChildClass();
            testLogger.Debug("Test {0}", testChildClass);

            mockRenderer.AssertWasCalled(p => p.Render(testChildClass));
        }

        [Fact]
        public void WalksTheClassHierarchyUpwardsIfNoRenderersAreFound()
        {
            var testLogger = new TestLogger(LogLevel.Debug);
            var mockRenderer = MockRepository.GenerateMock<IObjectRenderer>();
            testLogger.AddObjectRenderer<ObjectRendering>(mockRenderer);

            var childClassOfChildClass = new SubTestClass();
            testLogger.Debug("Test {0}", childClassOfChildClass);

            mockRenderer.AssertWasCalled(p => p.Render(childClassOfChildClass));
        }

        private class SubTestClass : TestChildClass
        {
            
        }
        private class TestChildClass : ObjectRendering
        {
            
        }
    }
}