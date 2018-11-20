using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.StringLibrary.Tests
{
    [TestClass]
    public class StringEnumExtensionsTests
    {
        #region Nullable 
        [TestMethod]
        public void TestStringToEnum_Nullable_Null_String()
        {
            // Arrange
            string enumAsString = null;

            // Act
            var actual = enumAsString.ToEnum<TestEnum>();

            // Assert
            Assert.IsNull(actual);
        }
        
        [TestMethod]
        public void TestStringToEnum_Nullable_Empty_String()
        {
            // Arrange
            var enumAsString = "";

            // Act
            var actual = enumAsString.ToEnum<TestEnum>();

            // Assert
            Assert.IsNull(actual);
        }


        [TestMethod]
        public void TestStringToEnum_Nullable_Whitespace_String()
        {
            // Arrange
            var enumAsString = "  \t";

            // Act
            var actual = enumAsString.ToEnum<TestEnum>();

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void TestStringToEnum_Nullable()
        {
            // Arrange
            var enumAsString = "Test1";

            // Act
            var actual = enumAsString.ToEnum<TestEnum>();

            // Assert
            Assert.AreEqual(TestEnum.Test1, actual);
        }

        [TestMethod]
        public void TestStringToEnum_Nullable_Numeric()
        {
            // Arrange
            var enumAsString = "0";

            // Act
            var actual = enumAsString.ToEnum<TestEnum>();

            // Assert
            Assert.AreEqual(TestEnum.Test1, actual);
        }

        [TestMethod]
        public void TestStringToEnum_Nullable_CaseSensitive()
        {
            // Arrange
            var enumAsString = "test4";

            // Act
            var actual = enumAsString.ToEnum<TestEnum>(false);

            // Assert
            Assert.IsNull(actual);
        }
        
        [TestMethod]
        public void TestStringToEnum_Nullable_NoNumericAllowed()
        {
            // Arrange
            var enumAsString = "3";

            // Act
            var actual = enumAsString.ToEnum<TestEnum>(true, false);

            // Assert
            Assert.IsNull(actual);
        }
        #endregion 

        #region Default Value (not nullable)
        [TestMethod]
        public void TestStringToEnum_Null_String()
        {
            // Arrange
            string enumAsString = null;

            // Act
            var actual = enumAsString.ToEnum(TestEnum.Test4);

            // Assert
            Assert.AreEqual(TestEnum.Test4, actual);
        }

        [TestMethod]
        public void TestStringToEnum_Empty_String()
        {
            // Arrange
            var enumAsString = "";

            // Act
            var actual = enumAsString.ToEnum(TestEnum.Test4);

            // Assert
            Assert.AreEqual(TestEnum.Test4, actual);
        }


        [TestMethod]
        public void TestStringToEnum_Whitespace_String()
        {
            // Arrange
            var enumAsString = "  \t";

            // Act
            var actual = enumAsString.ToEnum(TestEnum.Test4);

            // Assert
            Assert.AreEqual(TestEnum.Test4, actual);
        }

        [TestMethod]
        public void TestStringToEnum()
        {
            // Arrange
            var enumAsString = "Test1";

            // Act
            var actual = enumAsString.ToEnum(TestEnum.Test4);

            // Assert
            Assert.AreEqual(TestEnum.Test1, actual);
        }

        [TestMethod]
        public void TestStringToEnumNumeric()
        {
            // Arrange
            var enumAsString = "0";

            // Act
            var actual = enumAsString.ToEnum(TestEnum.Test4);

            // Assert
            Assert.AreEqual(TestEnum.Test1, actual);
        }

        [TestMethod]
        public void TestStringToEnumCaseSensitive()
        {
            // Arrange
            var enumAsString = "test4";

            // Act
            var actual = enumAsString.ToEnum(TestEnum.Test4, false);

            // Assert
            Assert.AreEqual(TestEnum.Test4, actual);
        }

        [TestMethod]
        public void TestStringToEnumNoNumericAllowed()
        {
            // Arrange
            var enumAsString = "3";

            // Act
            var actual = enumAsString.ToEnum(TestEnum.Test2, true, false);

            // Assert
            Assert.AreEqual(TestEnum.Test2, actual);
        }
        #endregion
    }
}
