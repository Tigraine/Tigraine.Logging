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