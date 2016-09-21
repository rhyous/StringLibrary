using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Rhyous.StringLibrary.Tests
{
    [TestClass]
    public class TrimPropertiesTests
    {
        public class TestObject
        {
            public int Id { get; set; }
            public string String1 { get; set; }
            public SubTestObject Sub { get; set; }
        }

        public class SubTestObject
        {
            public int Id { get; set; }
            public string String2 { get; set; }
        }

        class ObjectList : List<object>
        {
            public string Name { get; set; }
        }

        [TestMethod]
        public void RootObjectIsTrimmed()
        {
            // Arrange
            var testObj = new TestObject { Id = 1, String1 = " Blah    String1 ", Sub = new SubTestObject { Id = 2, String2 = "  Blah\tString2 " } };
            const string expected = "Blah String1";

            // Act
            testObj.TrimStringProperties();

            // Assert
            Assert.AreEqual(expected, testObj.String1);
        }

        [TestMethod]
        public void SubObjectIsTrimmed()
        {
            // Arrange
            var testObj = new TestObject { Id = 1, String1 = " Blah   String ", Sub = new SubTestObject { Id = 2, String2 = "  Blah\tString2 " } };
            const string expected = "Blah String2";

            // Act
            testObj.TrimStringProperties();

            // Assert
            Assert.AreEqual(expected, testObj.Sub.String2);
        }
        
        [TestMethod]
        public void StringPropertyOfGenericListIsTrimmed()
        {
            // Arrange
            var testObj = new ObjectList { Name = " List Name " };

            // Act
            testObj.TrimStringProperties();

            // Assert - doesn't throw exception and regular stuff is trimmed
            Assert.AreEqual("List Name", testObj.Name);
        }
    }
}
