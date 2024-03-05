using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Xml.Serialization;

namespace Rhyous.StringLibrary.Tests.Conversion
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
        [XmlTestDataSource(typeof(PrimitiveConversionRows), @"Data/PrimitiveConversions.xml", "Type,String")]
        public void ToGenericAndToTypeTest(PrimitiveConversionRow row)
        {
            // Arrange
            string s = row.String;
            var type = Type.GetType(row.Type);
            string expected =row.Expected;
            if (string.IsNullOrWhiteSpace(expected))
                expected = s;
            var message = row.Message;

            // Act
            var actual = s.ToType(type);

            // Assert
            Assert.AreEqual(type, actual.GetType());
            Assert.AreEqual(expected, actual.ToString(), message);
        }

        [XmlRoot("Row")]
        [XmlType("Row")]
        public class PrimitiveConversionRow
        {
            public string String { get; set; }
            public string Type { get; set; }
            public string Expected { get; set; }
            public string Message { get; set; }
        }

        [XmlRoot("Rows")]
        public class PrimitiveConversionRows : List<PrimitiveConversionRow>
        {
        }

        [TestMethod]
        public void ToDateTimeTest()
        {
            // Arrange
            string s = "01/01/2017";
            var expected = new DateTime(2017, 1, 1);

            // Act
            var actual = s.ToDate();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test converting a string to a DateTimeOffset.
        /// </summary>
        /// <remarks>
        /// During Daylight savings time (MDT), DateTimeOffset.Now.Offset (-0600) is one hour different 
        /// than DateTimeOffset.Parse("01/01/2017").OffSet (-0700). It was my expectation that they would
        /// be the same. I was disappointed that they were different. DateTime.Parse is not taking into
        /// account Daylight savings time. So the offset is required.
        /// </remarks>
        [TestMethod]
        public void ToDateTimeOffsetTest()
        {
            // Arrange
            var now = DateTimeOffset.Now;
            var offset = now.Offset;
            var offsetStr = now.ToString("zzz");
            string s = $"01/01/2017 00:00:00 {offsetStr}";
            var expected = new DateTimeOffset(2017, 1, 1, 0, 0, 0, offset);

            // Act
            var actual = s.ToDateTimeOffSet();

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

        [TestMethod]
        public void PrimitiveStringExtensions_0_To_Bool_Test()
        {
            var actual = "0".To<bool>();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void PrimitiveStringExtensions_1_To_Bool_Test()
        {
            var actual = "1".To<bool>();
            Assert.IsTrue(actual);
        }
    }
}