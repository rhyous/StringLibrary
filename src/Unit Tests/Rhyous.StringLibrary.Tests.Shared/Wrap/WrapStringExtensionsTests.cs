using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.StringLibrary.Tests.Shared.Wrap
{
    [TestClass]
    public class WrapStringExtensionsTests
    {
        [TestMethod]
        public void UnwrapQuotes()
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
        public void UnwrapDoublePound()
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
        public void UnwrapDoublePound_NothingUnwraps()
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
        public void UnwrapBrackets()
        {
            // Arrange
            string str = "<mystring>";
            var expected = "mystring";

            // Act
            var actual = str.Unwrap("'");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnwrapBracketsToEmptyString()
        {
            // Arrange
            string str = "<>";
            var expected = "";

            // Act
            var actual = str.Unwrap("'");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnwrapBracketsOneSideString()
        {
            // Arrange
            string str = "<";
            var expected = "<";

            // Act
            var actual = str.Unwrap("'");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnwrapXml()
        {
            // Arrange
            string str = "<node>value</node>";
            var expected = "value";

            // Act
            var actual = str.Unwrap("<node>", "</node>");

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
