using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.StringLibrary.Tests
{
    [TestClass]
    public class TrimListExtensionsTests
    {
        [TestMethod]
        public void TrimListExtensions_GenericStringListIsTrimmed()
        {
            // Arrange
            var list = new List<string> { " A ", " B ", " C " };

            // Act
            list.TrimStringProperties();

            // Assert - doesn't throw exception and regular stuff is trimmed
            Assert.AreEqual("A", list[0]);
            Assert.AreEqual("B", list[1]);
            Assert.AreEqual("C", list[2]);
        }

        [TestMethod]
        public void TrimListExtensions_TrimList_Test()
        {
            // Arrange
            var list = new List<TestObject> { new TestObject { Id = 1, Value = " Obj1 ", Sub = new SubTestObject { Id = 2, SubValue = " SubObj2 " } } };

            // Act
            list.TrimList();

            // Assert
            Assert.AreEqual("Obj1", list.First().Value);
            Assert.AreEqual("SubObj2", list.First().Sub.SubValue);
        }
    }
}
