using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Rhyous.StringLibrary.Tests
{
    [TestClass]
    public class StreamStringExtensionsTests
    {
        [TestMethod]
        public void StreamStringExtensions_AsString_Test()
        {
            // Arrange
            var a = "abc";

            // Act
            var stream = a.ToStream();
            var b = stream.AsString();

            // Assert
            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public async Task StreamStringExtensions_AsStringAsync_Test()
        {
            // Arrange
            var a = "abc";

            // Act
            var stream = a.ToStream();
            var b = await stream.AsStringAsync();

            // Assert
            Assert.AreEqual(a, b);
        }
    }
}