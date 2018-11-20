using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Rhyous.StringLibrary.Tests
{
    [TestClass]
    public class StringComparisonExtensionsTests
    {
        [TestMethod]
        public void StringComparisonExtensions_Contains_Tests()
        {
            // Arrange
            var s = "abcdefg";

            // Act
            var actual = s.Contains("CDE", StringComparison.OrdinalIgnoreCase);

            // Assert
            Assert.IsTrue(actual);
        }
    }
}
