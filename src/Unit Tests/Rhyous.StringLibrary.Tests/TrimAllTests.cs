using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.StringLibrary.Tests
{
    /// <summary>
    /// Example of String Parameter Value Coverage
    /// http://www.rhyous.com/2012/05/08/unit-testing-with-parameter-value-coverage-pvc/
    /// http://www.rhyous.com/2015/05/08/row-tests-or-paramerterized-tests-mstest-csv/
    /// </summary>
    [TestClass]
    public class TrimAllTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"Data\ParameterValueStrings.xml", "Row", DataAccessMethod.Sequential)]
        public void TestTrimAll()
        {
            // Arrange
            string stringWithInvalidSpace = TestContext.DataRow[0].ToString().Unquote();
            string expectedTrimmedString = TestContext.DataRow[1].ToString().Unquote();
            string message = TestContext.DataRow[2].ToString().Unquote();

            // Act
            var actual = stringWithInvalidSpace.TrimAll();

            // Assert
            Assert.AreNotEqual(stringWithInvalidSpace, expectedTrimmedString);
            Assert.AreEqual(expectedTrimmedString, actual, message);
        }
    }
}
