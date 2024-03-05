
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace Rhyous.StringLibrary.Tests.Expression
{
    [TestClass]
    public class PropertyNameLambdaExtensionsTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [XmlTestDataSource(typeof(LambdaStringRows), @"Data/LambdaStrings.xml", "ExpectedExpression")]
        public void PropertyNameLambdaExtensions_TestToLambda_TypeParams(LambdaStringRow row)
        {
            // Arrange
            var a = new ModelA { Id = 1, Name = "ABC", Date = new DateTime(2017, 1, 1), Guid = new Guid("{C1AA6176-425D-4981-BE4A-8F5C459E0FF9}") };
            var prop = row.Property;
            var type = Type.GetType(row.Type);
            var value = row.Value.ToType(type, null, new CultureInfo("en-US"));
            var method = row.Method;
            var expectedExpression = row.ExpectedExpression;
            if (expectedExpression.Contains("{0}"))
                expectedExpression = string.Format(expectedExpression, value);
            var expectedResult = row.ExpectedResult.To<bool>();
            var message = row.Message;

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
            var value = "01/01/2017".ToType(type, null, new CultureInfo("en-US"));
            var method = "le";
            var expectedExpression = $"e => (e.Date <= {value})";
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

    [XmlRoot("Row")]
    [XmlType("Row")]
    public class LambdaStringRow
    {
        public string Property { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string Method { get; set; }
        public string ExpectedExpression { get; set; }
        public string ExpectedResult { get; set; }
        public string Message { get; set; }
    }

    [XmlRoot("Rows")]
    public class LambdaStringRows : List<LambdaStringRow>
    {
    }
}