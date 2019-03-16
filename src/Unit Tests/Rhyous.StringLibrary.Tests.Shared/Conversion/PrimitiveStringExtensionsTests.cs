#if NETCOREAPP2_0
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace Rhyous.StringLibrary.Tests.Comparison
{
    [TestClass]
    public class PrimitiveStringExtensionsTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void PrimitiveStringExtensions_To_Int_Test()
        {
            Assert.AreEqual(10, "10".To<int>());
        }

        [TestMethod]
        public void PrimitiveStringExtensions_To_ObjectClass_Test()
        {
            Assert.IsNull("10".To<object>());
        }

        /// <summary>
        /// Tests both the ToGeneric and the ToType methods
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"Data\PrimitiveConversions.xml", "Row", DataAccessMethod.Sequential)]
        public void ToGenericAndToTypeTest()
        {
            // Arrange
            string s = TestContext.DataRow[0].ToString();
            var type = Type.GetType(TestContext.DataRow[1].ToString());
            string expected = TestContext.DataRow[2].ToString();
            if (string.IsNullOrWhiteSpace(expected))
                expected = s;

            // Act
            var actual = s.ToType(type);

            // Assert
            Assert.AreEqual(type, actual.GetType());
            Assert.AreEqual(expected, actual.ToString());
        }

        [TestMethod]
        public void ToDateTimeTest()
        {
            // Arrange
            string s = "01/01/2017";
            var expected = new DateTime(2017,1,1);

            // Act
            var actual = s.ToDate();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToGenericAndToType_Enum_Test()
        {
            // Arrange
            string s = "1";
            var type = typeof(TestEnum);
            string expected = "Test2";

            // Act
            var actual = s.ToType(type);

            // Assert
            Assert.AreEqual(type, actual.GetType());
            Assert.AreEqual(expected, actual.ToString());
        }

        [TestMethod]
        public void ToGenericAndToType_Nullable_Test()
        {
            // Arrange
            string s = "";
            var type = typeof(int?);
            int? n = 1;

            // Act
            int? actual = s.To(n);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void ToGenericAndToType_7_Test()
        {
            // Arrange
            string s = "7";
            var type = typeof(int?);
            int? n = 1;

            // Act
            int? actual = s.To(n);

            // Assert
            Assert.AreEqual(7, actual);
        }
    }
}
#endif