using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rhyous.StringLibrary.Tests.Shared.Enum
{
    [TestClass]
    public class StringEnumExtensionsTests
    {
        [TestMethod]
        public void TestStringToEnum()
        {
            // Arrange
            var enumAsString = "Test1";

            // Act
            var actual = enumAsString.ToEnum<TestEnum>();

            // Assert
            Assert.AreEqual(TestEnum.Test1, actual);
        }

        [TestMethod]
        public void TestStringToEnumNumeric()
        {
            // Arrange
            var enumAsString = "0";

            // Act
            var actual = enumAsString.ToEnum<TestEnum>();

            // Assert
            Assert.AreEqual(TestEnum.Test1, actual);
        }

        [TestMethod]
        public void TestStringToEnumCaseSensitive()
        {
            // Arrange
            var enumAsString = "test4";

            // Act
            var actual = enumAsString.ToEnum<TestEnum>(false);

            // Assert
            Assert.IsNull(actual);
        }
        
        [TestMethod]
        public void TestStringToEnumNoNumericAllowed()
        {
            // Arrange
            var enumAsString = "3";

            // Act
            var actual = enumAsString.ToEnum<TestEnum>(true, false);

            // Assert
            Assert.IsNull(actual);
        }
    }
}
