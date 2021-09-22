using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace Rhyous.StringLibrary.Tests
{
    [TestClass]
    public class StringEncodingExtensionsTests
    {
        #region Base64Encode
        [TestMethod]
        public void StringEncodingExtensions_Base64Encode_Null_String_Returns_Null()
        {
            // Arrange
            string str = null;
            Encoding encoding = Encoding.UTF8;

            // Act
            var result = str.Base64Encode(encoding);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void StringEncodingExtensions_Base64Encode_EmptyString_Returns_EmptyString()
        {
            // Arrange
            string str = "";
            Encoding encoding = Encoding.UTF8;

            // Act
            var result = str.Base64Encode(encoding);

            // Assert
            Assert.AreEqual("", result);
        }


        [TestMethod]
        public void StringEncodingExtensions_Base64Encode_ValidString_Test()
        {
            // Arrange
            string str = "abc+xyz";
            Encoding encoding = Encoding.UTF8;

            // Act
            var result = str.Base64Encode(encoding);

            // Assert
            Assert.AreEqual("YWJjK3h5eg==", result);
        }
        #endregion

        #region Base64Decode

        [TestMethod]
        public void StringEncodingExtensions_Base64Decode_Null_String_Returns_Null()
        {
            // Arrange
            string str = null;
            Encoding encoding = Encoding.UTF8;

            // Act
            var result = str.Base64Decode(encoding);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void StringEncodingExtensions_Base64Decode_EmptyString_Returns_EmptyString()
        {
            // Arrange
            string str = "";
            Encoding encoding = Encoding.UTF8;

            // Act
            var result = str.Base64Decode(encoding);

            // Assert
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void StringEncodingExtensions_Base64Decode_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string ecodedStr = "YWJjK3h5eg==";
            Encoding encoding = null;

            // Act
            var result = ecodedStr.Base64Decode(encoding);

            // Assert
            Assert.AreEqual("abc+xyz", result);
        }
        #endregion
    }
} 