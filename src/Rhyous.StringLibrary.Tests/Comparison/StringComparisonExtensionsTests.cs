using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Rhyous.StringLibrary.Tests
{
    [TestClass]
    public class StringComparisonExtensionsTests
    {
        #region Contains

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

        [TestMethod]
        public void StringComparisonExtensions_ContainsAny_CaseSensitive_Tests()
        {
            // Arrange
            var s = "abcdefg";

            // Act
            var actual = s.ContainsAny("CDE", "Efg");

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void StringComparisonExtensions_ContainsAny_CaseInsensitive_Tests()
        {
            // Arrange
            var s = "abcdefg";

            // Act
            var actual = s.ContainsAny(StringComparison.OrdinalIgnoreCase, "CDE", "Efg");

            // Assert
            Assert.IsTrue(actual);
        }
        #endregion

        #region StartsWithAny
        [TestMethod]
        public void StringComparisonExtensions_StartsWithAny_CaseSensitive_Tests()
        {
            // Arrange
            var s = "abcdefg";

            // Act
            var actual = s.StartsWithAny("ABC", "AB");

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void StringComparisonExtensions_StartsWithAny_CaseInsensitive_Tests()
        {
            // Arrange
            var s = "abcdefg";

            // Act
            var actual = s.StartsWithAny(StringComparison.OrdinalIgnoreCase, "ABC", "AB");

            // Assert
            Assert.IsTrue(actual);
        }
        #endregion

        #region EndsWithAny
        [TestMethod]
        public void StringComparisonExtensions_EndsWithAny_CaseSensitive_Tests()
        {
            // Arrange
            var s = "abcdefg";

            // Act
            var actual = s.EndsWithAny("EFG", "FG");

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void StringComparisonExtensions_EndsWithAny_CaseInsensitive_Tests()
        {
            // Arrange
            var s = "abcdefg";

            // Act
            var actual = s.EndsWithAny(StringComparison.OrdinalIgnoreCase, "EFG", "FG");

            // Assert
            Assert.IsTrue(actual);
        }
        #endregion
    }
}
