using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.UnitTesting;

namespace Rhyous.StringLibrary.Tests.Json
{
    [TestClass]
    public class StringJsonExtensionsTests
    {
        #region IsJsonArray
        [TestMethod]
        [StringIsNullEmptyOrWhitespace]
        public void StringJsonExtensions_IsJsonArray_stringValue_IsNullEmptyOrWhitespace_ReturnsFalse(string value)
        {
            Assert.IsFalse(value.IsJsonArray());
        }

        [TestMethod]
        [PrimitiveList("[", "]", "No bracket")]
        public void StringJsonExtensions_IsJsonArray_stringValue_IsNotAJsonArray_ReturnsFalse(string value)
        {
            Assert.IsFalse(value.IsJsonArray());
        }

        [TestMethod]
        [PrimitiveList("[]", " [] ", "[  ]", "[ \"a\": \"a1\"]", "[not really json but has brackets showing naivity]")]
        public void StringJsonExtensions_IsJsonArray_stringValue_IsJsonArray_ReturnsFalse(string value)
        {
            Assert.IsTrue(value.IsJsonArray());
        }
        #endregion

        #region IsEmptyJsonArray
        [TestMethod]
        [StringIsNullEmptyOrWhitespace]
        public void StringJsonExtensions_IsEmptyJsonArray_stringValue_IsNullEmptyOrWhitespace_ReturnsFalse(string value)
        {
            Assert.IsFalse(value.IsEmptyJsonArray());
        }

        [TestMethod]
        [PrimitiveList("[", "]", "No bracket")]
        public void StringJsonExtensions_IsEmptyJsonArray_stringValue_IsNotAJsonArray_ReturnsFalse(string value)
        {
            Assert.IsFalse(value.IsEmptyJsonArray());
        }

        [TestMethod]
        [PrimitiveList("[]", " [] ", "[  ]", "[ \r\n ]")]
        public void StringJsonExtensions_IsEmptyJsonArray_stringValue_IsEmptyJsonArray_ReturnsFalse(string value)
        {
            Assert.IsTrue(value.IsEmptyJsonArray());
        }
        #endregion


        #region IsJsonObject
        [TestMethod]
        [StringIsNullEmptyOrWhitespace]
        public void StringJsonExtensions_IsJsonObject_stringValue_IsNullEmptyOrWhitespace_ReturnsFalse(string value)
        {
            Assert.IsFalse(value.IsJsonObject());
        }

        [TestMethod]
        [PrimitiveList("{", "}", "No bracket")]
        public void StringJsonExtensions_IsJsonObject_stringValue_IsNotAJsonObject_ReturnsFalse(string value)
        {
            Assert.IsFalse(value.IsJsonObject());
        }

        [TestMethod]
        [PrimitiveList("{}", " {} ", "{  }", "{ \"a\": \"a1\"}", "{not really json but has brackets showing naivity}")]
        public void StringJsonExtensions_IsJsonObject_stringValue_IsJsonObject_ReturnsFalse(string value)
        {
            Assert.IsTrue(value.IsJsonObject());
        }
        #endregion

        #region IsEmptyJsonObject
        [TestMethod]
        [StringIsNullEmptyOrWhitespace]
        public void StringJsonExtensions_IsEmptyJsonObject_stringValue_IsNullEmptyOrWhitespace_ReturnsFalse(string value)
        {
            Assert.IsFalse(value.IsEmptyJsonObject());
        }

        [TestMethod]
        [PrimitiveList("{", "}", "No bracket")]
        public void StringJsonExtensions_IsEmptyJsonObject_stringValue_IsNotAJsonObject_ReturnsFalse(string value)
        {
            Assert.IsFalse(value.IsEmptyJsonObject());
        }

        [TestMethod]
        [PrimitiveList("{}", " {} ", "{  }", "{ \r\n }")]
        public void StringJsonExtensions_IsEmptyJsonObject_stringValue_IsEmptyJsonObject_ReturnsFalse(string value)
        {
            Assert.IsTrue(value.IsEmptyJsonObject());
        }
        #endregion
    }
}
