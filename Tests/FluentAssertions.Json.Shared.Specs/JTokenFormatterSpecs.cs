using FluentAssertions.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace FluentAssertions.Json
{
    [TestClass]
    // ReSharper disable InconsistentNaming
    public class JTokenFormatterSpecs
    {
        [TestMethod]
        public void Should_Handle_JToken()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var sut = new JTokenFormatter();

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            var actual = sut.CanHandle(JToken.Parse("{}"));
            
            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Should_not_handle_anything_else()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var testCases = new object[] { null, string.Empty };
            var sut = new JTokenFormatter();

            foreach (var testCase in testCases)
            {
                //-----------------------------------------------------------------------------------------------------------
                // Act
                //-----------------------------------------------------------------------------------------------------------
                var actual = sut.CanHandle(testCase);

                //-----------------------------------------------------------------------------------------------------------
                // Assert
                //-----------------------------------------------------------------------------------------------------------
                actual.Should().BeFalse();
            }
        }


        [TestMethod]
        public void Should_preserve_indenting()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var json = JToken.Parse("{ \"id\":1 }");
            var sut = new JTokenFormatter();

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            var actual = sut.ToString(json, true);

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            actual.Should().Be(json.ToString(Newtonsoft.Json.Formatting.Indented));
        }

        [TestMethod]
        public void Should_Remove_line_breaks_and_indenting()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var json = JToken.Parse("{ \"id\":1 }");
            var sut = new JTokenFormatter();

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            // ReSharper disable once RedundantArgumentDefaultValue
            var actual = sut.ToString(json, false);
            
            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            actual.Should().Be(json.ToString().RemoveNewLines());
        }
    }
}