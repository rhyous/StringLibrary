using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.StringLibrary.Tests.Wrap
{
    [TestClass]
    public class QuoteStringExtensionsTests
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
        public void UnquoteNullStringTest()
        {
            // Arrange
            string str = null;

            // Act
            var actual = str.Unquote();

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void UnquoteEmptyStringTest()
        {
            // Arrange
            string str = "";
            var expected = "";

            // Act
            var actual = str.Unquote();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnquoteOnlyQuoteTest()
        {
            // Arrange
            string str = "'";
            var expected = "'";

            // Act
            var actual = str.Unquote();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnquoteOnlyQuotesTest()
        {
            // Arrange
            string str = "''";
            var expected = "";

            // Act
            var actual = str.Unquote();

            // Assert
            Assert.AreEqual(expected, actual);
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
        public void UnquoteOnlyQuotesOneLevelTest()
        {
            // Arrange
            string str = "''''";
            var expected = "''";

            // Act
            var actual = str.Unquote(1);

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


        [TestMethod]
        public void UnquoteDoesNotRemoveOneQuoteAtFrontOrBackOfString_NotQuoted()
        {
            // Arrange
            var str = "'N Crafts";
            var expected = "'N Crafts";

            // Act
            var actual = str.Unquote();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnquoteDoesNotRemoveOneQuoteAtFrontOrBackOfString_Quoted()
        {
            // Arrange
            var str = "\"'N Crafts\"";
            var expected = "'N Crafts";

            // Act
            var actual = str.Unquote();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EscapeQuote_WhenFirstCharIsQuoteButLastCharIsNot()
        {
            // Arrange
            var str = "'N Crafts";
            var expected = "''N Crafts";

            // Act
            var actual = str.EscapeQuotes('\'');

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EscapeQuotesInsideString()
        {
            // Arrange
            var str = "Surf 'n Turf";
            var expected = "Surf ''n Turf";

            // Act
            var actual = str.EscapeQuotes('\'');

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EscapeQuote_WhenFirstCharIsQuoteButLastCharIsNot_Quoted()
        {
            // Arrange
            var str = "\"'N Crafts\"";
            var expected = "\"''N Crafts\"";

            // Act
            var actual = str.EscapeQuotes('\'');

            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void EscapeQuote_WhenLastCharIsQuoteButLastCharIsNot_Quoted()
        {
            // Arrange
            var str = "\"Runnin'\"";
            var expected = "\"Runnin''\"";

            // Act
            var actual = str.EscapeQuotes('\'');

            // Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void EscapeQuotesInsideString_Quoted()
        {
            // Arrange
            var str = "\"Surf 'n Turf\"";
            var expected = "\"Surf ''n Turf\"";

            // Act
            var actual = str.EscapeQuotes('\'');

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnescapeQuotesInsideString()
        {
            // Arrange
            var str = "\"Surf ''n Turf\"";
            var expected = "\"Surf 'n Turf\"";

            // Act
            var actual = str.UnescapeQuotes();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnescapeQuotesInsideString_DoubleQuotes()
        {
            // Arrange
            var str = "The \"\"ugly town\"\" is this way.\"";
            var expected = "The \"ugly town\" is this way.\"";

            // Act
            var actual = str.UnescapeQuotes();
            // Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void UnescapeQuotesInsideString_DoubleQuotes_SlashEscapeChar()
        {
            // Arrange
            var str = "The \\\"ugly town\\\" is this way.\"";
            var expected = "The \"ugly town\" is this way.\"";

            // Act
            var actual = str.UnescapeQuotes('\"', '\\');

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnescapeQuote_WhenFirstCharIsQuoteButLastCharIsNot_Quoted()
        {
            // Arrange
            var str = "\"''N Crafts\"";
            var expected = "\"'N Crafts\"";

            // Act
            var actual = str.UnescapeQuotes();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnescapeQuote_WhenLastCharIsQuoteButLastCharIsNot_Quoted()
        {
            // Arrange
            var str = "\"Runnin''\"";
            var expected = "\"Runnin'\"";

            // Act
            var actual = str.UnescapeQuotes();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnescapeQuote_WhenFirstCharIsQuoteButLastCharIsNot_Quoted_CharacterSpecified()
        {
            // Arrange
            var str = "\"''N Crafts\"";
            var expected = "\"'N Crafts\"";

            // Act
            var actual = str.UnescapeQuotes('\'', '\'');

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnescapeQuote_WhenLastCharIsQuoteButLastCharIsNot_Quoted_CharacterSpecified()
        {
            // Arrange
            var str = "\"Runnin''\"";
            var expected = "\"Runnin'\"";

            // Act
            var actual = str.UnescapeQuotes('\'', '\'');

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
