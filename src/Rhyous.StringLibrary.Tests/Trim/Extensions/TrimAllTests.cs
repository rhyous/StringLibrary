using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.StringLibrary.Tests.Expression;
using Rhyous.UnitTesting;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Rhyous.StringLibrary.Tests
{
    [TestClass]
    public class TrimAllTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [XmlTestDataSource(typeof(ParameterValueStringRows), @"Data\ParameterValueStrings.xml", "Value")]
        public void TestTrimAll(ParameterValueStringRow row)
        {
            // Arrange
            string stringWithInvalidSpace = row.Value.Unquote();
            string expectedTrimmedString = row.ExpectedValue.Unquote();
            string message = row.Message.Unquote();

            // Act
            var actual = stringWithInvalidSpace.TrimAll();

            // Assert
            Assert.AreNotEqual(stringWithInvalidSpace, expectedTrimmedString);
            Assert.AreEqual(expectedTrimmedString, actual, message);
        }
    }

    [XmlRoot("Row")]
    [XmlType("Row")]
    public class ParameterValueStringRow
    {
        public string Value { get; set; }
        public string ExpectedValue { get; set; }
        public string Message { get; set; }
    }

    [XmlRoot("Rows")]
    public class ParameterValueStringRows : List<ParameterValueStringRow>
    {
    }
}