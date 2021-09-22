#if NET461
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.StringLibrary.Tests.Expression
{
    [TestClass]
    public class PropertyNameLambdaExtensionsTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"Data\LambdaStrings.xml", "Row", DataAccessMethod.Sequential)]
        public void PropertyNameLambdaExtensions_TestToLambda_TypeParams()
        {
            // Arrange
            var a = new ModelA { Id = 1, Name = "ABC", Date = new DateTime(2017, 1, 1), Guid = new Guid("{C1AA6176-425D-4981-BE4A-8F5C459E0FF9}") };
            var prop = TestContext.DataRow["Property"].ToString();
            var type = Type.GetType(TestContext.DataRow["Type"].ToString());
            var value = TestContext.DataRow["Value"].ToString().ToType(type);
            var method = TestContext.DataRow["Method"].ToString();
            var expectedExpression = TestContext.DataRow["ExpectedExpression"].ToString();
            var expectedResult = TestContext.DataRow["ExpectedResult"].ToString().ToBool();
            var message = TestContext.DataRow["Message"].ToString();

            // Act
            var expression = prop.ToLambda<ModelA>(type, new object[] { value, method });
            var compiledLambda = expression.Compile();
            var result = (bool)compiledLambda.DynamicInvoke(a);
            // Assert
            Assert.AreEqual(expectedExpression, expression.ToString(), message);
            Assert.AreEqual(expectedResult, result, message);
        }

        [TestMethod]
        public void PropertyNameLambdaExtensions_TestToLambda_Type_One_Params()
        {
            // Arrange
            var a = new ModelA { Id = 1, Name = "ABC", Date = new DateTime(2017, 1, 1), Guid = new Guid("{C1AA6176-425D-4981-BE4A-8F5C459E0FF9}") };
            var prop = "Date";
            var type = typeof(DateTime);
            var value = "01/01/2017".ToType(type);
            var method = "le";
            var expectedExpression = "e => (e.Date <= 1/1/2017 12:00:00 AM)";
            var expectedResult = true;
            var message = "Date is 01/01/2017 which is not less than or equal to 12/31/2016.";

            // Act
            var expression = prop.ToLambda<ModelA>(type, new object[] { value, method });
            var compiledLambda = expression.Compile();
            var result = (bool)compiledLambda.DynamicInvoke(a);

            // Assert
            Assert.AreEqual(expectedExpression, expression.ToString(), message);
            Assert.AreEqual(expectedResult, result, message);
        }

        #region Class
        public class ModelA
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public Guid Guid { get; set; }
        }
        #endregion

    }
}
#endif