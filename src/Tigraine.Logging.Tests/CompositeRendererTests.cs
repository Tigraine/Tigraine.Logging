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
    using ObjectRenderers;
    using Xunit;

    public class CompositeRendererTests
    {
        [Fact]
        public void CompositeRendererCallsFuncToRender()
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