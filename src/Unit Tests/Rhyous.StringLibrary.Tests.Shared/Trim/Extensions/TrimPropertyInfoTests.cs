using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Reflection;

namespace Rhyous.StringLibrary.Tests
{
    [TestClass]
    public class TrimPropertyInfoExtensionsTests
    {
        [TestMethod]
        public void TrimPropertyInfoExtensions_IsTrimmable_NullObjectDoesNotThrowException_Test()
        {
            // Arrange
            PropertyInfo pi = null;

            // Act
            var actual = pi.IsTrimmable();

            // Assert 
            Assert.IsFalse(actual);
        }
    }
}