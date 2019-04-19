using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
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

#if NETCOREAPP2_0
#else
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
#endif

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

        [TestMethod]
        public void PrimitiveStringExtensions_DoubleString_To_Int_enUS_Test()
        {
            var actual = "1.000".To(0, CultureInfo.GetCultureInfoByIetfLanguageTag("en-US"));
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void PrimitiveStringExtensions_DoubleString_To_Int_Generic_enUS_Test()
        {
            var actual = "1.000".To<int>(0, CultureInfo.GetCultureInfoByIetfLanguageTag("en-US"));
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void PrimitiveStringExtensions_DoubleString_To_Int16_Generic_enUS_Test()
        {
            var actual = "1.000".To<short>(0, CultureInfo.GetCultureInfoByIetfLanguageTag("en-US"));
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void PrimitiveStringExtensions_DoubleString_RoundUp_To_Int_enUS_Test()
        {
            var actual = "1.600".To(0, CultureInfo.GetCultureInfoByIetfLanguageTag("en-US"));
            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void PrimitiveStringExtensions_DoubleString_To_Int_enGB_Test()
        {
            var actual = "1.000".To(0, CultureInfo.GetCultureInfoByIetfLanguageTag("en-GB"));
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void PrimitiveStringExtensions_DoubleString_To_Int_Invariant_Test()
        {
            var actual = "1.000".To(0);
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void PrimitiveStringExtensions_1_To_Int_Test()
        {
            var actual = "1".To<int>();
            Assert.AreEqual(1, actual);
        }
    }
}