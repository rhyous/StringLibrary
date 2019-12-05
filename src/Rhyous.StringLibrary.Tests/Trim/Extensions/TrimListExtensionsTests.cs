using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.StringLibrary.Tests
{
    [TestClass]
    public class TrimListExtensionsTests
    {
        [TestMethod]
        public void TrimListExtensions_TrimList_Test()
        {
            // Arrange
            var list = new List<TestObject> { new TestObject { Id = 1, String1 = " Obj1 ", Sub = new SubTestObject { Id = 2, String2 = " SubObj2 " } } };

            // Act
            list.TrimList();

            // Assert
            Assert.AreEqual("Obj1", list.First().String1);
            Assert.AreEqual("SubObj2", list.First().Sub.String2);
        }
    }
}
