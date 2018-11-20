using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.StringLibrary.Tests
{
    [TestClass]
    public class StreamStringExtensionsTests
    {
        [TestMethod]
        public void StreamStringExtensionsTest()
        {
            // Arrange
            var a = "abc";

            // Act
            var stream = a.ToStream();
            var b = stream.AsString();

            // Assert
            Assert.AreEqual(a, b);
        }
    }
}