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
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"Data\PrimitiveConversions.xml", "Row", DataAccessMethod.Sequential)]
        public void ToGenericTest()
        {
            // Arrange
            string s = TestContext.DataRow[0].ToString();
            string strType = TestContext.DataRow[1].ToString();
            var type = Type.GetType(strType);
            string expected = TestContext.DataRow[2].ToString();
            if (string.IsNullOrWhiteSpace(expected))
                expected = s;

            // Act
            MethodInfo mi = typeof(PrimitiveStringExtensions).GetMethod("To");
            MethodInfo method = mi.MakeGenericMethod(type);
            var actual = method.Invoke(null, new object[] { s, null });

            // Assert
            Assert.AreEqual(type, actual.GetType());
            Assert.AreEqual(expected, actual.ToString());
        }
    }
}
