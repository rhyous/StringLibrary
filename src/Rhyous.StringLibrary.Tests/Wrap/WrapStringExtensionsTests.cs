using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.StringLibrary.Tests.Wrap
{
    [TestClass]
    public class WrapStringExtensionsTests
    {

        #region IsWrapped
        [TestMethod]
        public void WrapStringExtensions_IsWrapped_Single()
        {
            // Arrange

            string value = "\"double wrapped\"";
            string[] wraps = ["\""];

            // Act & Assert
            Assert.IsTrue(value.IsWrapped(wraps));
        }

        [TestMethod]
        public void WrapStringExtensions_IsWrapped_Double_SameWrapper()
        {
            // Arrange

            string value = "\"\"double wrapped\"\"";
            string[] wraps = ["\""];

            // Act & Assert
            Assert.IsTrue(value.IsWrapped(wraps));
        }

        [TestMethod]
        public void WrapStringExtensions_IsWrapped_Triple_SameWrapper()
        {
            // Arrange

            string value = "\"\"\"triplewrapped\"\"\"";
            string[] wraps = ["\""];

            // Act & Assert
            Assert.IsTrue(value.IsWrapped(wraps));
        }

        [TestMethod]
        public void WrapStringExtensions_IsWrapped_Double_DifferentWrappers()
        {
            // Arrange

            string value = "\"'double wrapped'\"";
            string[] wraps = ["\"", "'"];

            // Act & Assert
            Assert.IsTrue(value.IsWrapped(wraps));
        }

        [TestMethod]
        public void WrapStringExtensions_IsWrapped_Triple_DifferentWrappers()
        {
            // Arrange

            string value = "\"'# triplewrapped#'\"";
            string[] wraps = ["\"", "'", "#"];

            // Act & Assert
            Assert.IsTrue(value.IsWrapped(wraps));
        }
        #endregion

        #region Wrap(this string value, string surround)
        [TestMethod]
        public void WrapStringExtensions_Wrap_SurroundString()
        {
            // Arrange
            string value = "SomeString";
            string surround = "#";
            var expected = "#SomeString#";

            // Act
            var result = value.Wrap(surround);

            // Assert
            Assert.AreEqual(expected, result);
        }
        #endregion

        #region Wrap(this string value, char surround)
        [TestMethod]
        public void WrapStringExtensions_Wrap_SurroundChar()
        {
            // Arrange
            string value = "SomeString";
            char surround = '#';
            var expected = "#SomeString#";

            // Act
            var result = value.Wrap(surround);

            // Assert
            Assert.AreEqual(expected, result);
        }
        #endregion

        #region Wrap(this string value, string prefix, string postfix)
        [TestMethod]
        public void WrapStringExtensions_Wrap_PrefixPostfixStrings()
        {
            // Arrange
            string value = "node";
            string prefix = "<";
            string postfix = "/>";
            var expected = "<node/>";

            // Act
            var result = value.Wrap(prefix, postfix);

            // Assert
            Assert.AreEqual(expected, result);
        }
        #endregion

        #region Wrap(this string value, char prefix, char postfix)
        [TestMethod]
        public void WrapStringExtensions_Wrap_PrefixPostfixChars()
        {
            // Arrange
            string value = "node";
            char prefix = '<';
            char postfix = '>';
            var expected = "<node>";

            // Act
            var result = value.Wrap(prefix, postfix);

            // Assert
            Assert.AreEqual(expected, result);
        }
        #endregion

        #region Unwrap(this string value, string wrap)
        [TestMethod]
        public void Unwrap_Quotes()
        {
            // Arrange
            string str = "''''";
            var expected = "''";

            // Act
            var actual = str.Unwrap("'");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Unwrap_DoublePound()
        {
            // Arrange
            string str = "##SomeText##";
            var expected = "SomeText";

            // Act
            var actual = str.Unwrap("##");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Unwrap_DoublePound_NothingUnwraps()
        {
            // Arrange
            string str = "###";
            var expected = "###";

            // Act
            var actual = str.Unwrap("##");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Unwrap_Brackets()
        {
            // Arrange
            string str = "<mystring>";
            var expected = "<mystring>";

            // Act
            var actual = str.Unwrap("'");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Unwrap_BracketsToEmptyString()
        {
            // Arrange
            string str = "<>";
            var expected = "<>";

            // Act
            var actual = str.Unwrap("'");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Unwrap_BracketsOneSideString()
        {
            // Arrange
            string str = "<";
            var expected = "<";

            // Act
            var actual = str.Unwrap("'");

            // Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Unwrap(this string value, char wrap)
        [TestMethod]
        public void Unwrap_char_Quotes()
        {
            // Arrange
            string str = "''''";
            var expected = "''";

            // Act
            var actual = str.Unwrap('\'');

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Unwrap_char_NotWrappedInTheWrapCharString()
        {
            // Arrange
            string str = "<>";
            var expected = "<>";

            // Act
            var actual = str.Unwrap("'");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Unwrap_char_BracketsOneSideString()
        {
            // Arrange
            string str = "<";
            var expected = "<";

            // Act
            var actual = str.Unwrap("'");

            // Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Unwrap(this string value, string prefix, string postfix)

        [TestMethod]
        public void Unwrap_Xml()
        {
            // Arrange
            string str = "<node>value</node>";
            var expected = "value";

            // Act
            var actual = str.Unwrap("<node>", "</node>");

            // Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Unwrap(this string value, char prefix, char postfix)

        [TestMethod]
        public void Unwrap_char_Xml()
        {
            // Arrange
            string str = "<node>";
            var expected = "node";

            // Act
            var actual = str.Unwrap('<', '>');

            // Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
