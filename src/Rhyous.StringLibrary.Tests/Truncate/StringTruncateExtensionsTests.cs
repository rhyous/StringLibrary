using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.UnitTesting;
using System;

namespace Rhyous.StringLibrary.Tests.Truncate
{
    [TestClass]
    public class StringTruncateExtensionsTests
    {
        [TestMethod]
        [StringIsNullEmptyOrWhitespace]
        public void Truncate_NullEmptyOrWhitespace_NoChange_Test(string s)
        {
            // Arrange
            int maxLength = 10;

            // Act
            var result = s.Truncate(maxLength);

            // Assert
            Assert.AreEqual(s, result);
        }

        [TestMethod]
        public void Truncate_ShortString_NoChange_Test()
        {
            // Arrange
            int maxLength = 10;
            var s = "ABC";
            // Act
            var result = s.Truncate(maxLength);

            // Assert
            Assert.AreEqual(s, result);
        }

        [TestMethod]
        public void Truncate_LongString_Truncated_Test()
        {
            // Arrange
            int maxLength = 10;
            var s = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var expected = "ABCDEFGHIJ";

            // Act
            var result = s.Truncate(maxLength);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
