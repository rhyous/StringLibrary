using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.StringLibrary.Tests.Shared.Wrap
{
    [TestClass]
    public class WrapStringExtensionsTests
    {
        [TestMethod]
        public void IsQuotedTrueSingleTest()
        {
            Assert.IsTrue("'QuotedString'".IsQuoted());
        }

        [TestMethod]
        public void IsQuotedTrueDoubleTest()
        {
            Assert.IsTrue("\"QuotedString\"".IsQuoted());
        }

        [TestMethod]
        public void UnquoteSingleTest()
        {
            // Arrange
            var str = "'some string'";
            var expected = "some string";

            // Act
            var actual = str.Unquote();
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnquoteDoubleTest()
        {
            // Arrange
            var str = "\"some string\"";
            var expected = "some string";

            // Act
            var actual = str.Unquote();
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnquoteNoQuotesDoesNothingTest()
        {
            // Arrange
            var str = "some string";
            var expected = "some string";

            // Act
            var actual = str.Unquote();
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void QuoteTest()
        {
            // Arrange
            var str = "some string";
            var expected = "\"some string\"";

            // Act
            var actual = str.Quote();
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void QuoteSingleTest()
        {
            // Arrange
            var str = "some string";
            var expected = "'some string'";

            // Act
            var actual = str.Quote('\'');
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void QuoteAlreadyQuotedTest()
        {
            // Arrange
            var str = "\"some string\"";
            var expected = "\"some string\"";

            // Act
            var actual = str.Quote();
            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
