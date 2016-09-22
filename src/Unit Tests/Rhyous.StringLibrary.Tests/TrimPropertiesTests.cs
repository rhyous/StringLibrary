using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

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

        public class Getter
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Get { get { return " This cannot be set and cannot be trimmed. "; } }
        }

        public class ObjectList : List<object>
        {
            public string Name { get; set; }
        }

        public class ParentOfDictionary
        {
            public string Name { get; set; }
            public Dictionary<string, object> Map { get; set; } = new Dictionary<string, object>();
        }

        public class ObjectProperties
        {
            public string Name { get; set; }
            public object StringAsObject { get; set; }
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
        public void GetterOnlyPropertiesAreIgnored()
        {
            // Arrange
            var testObj = new Getter();

            // Act
            testObj.TrimStringProperties();

            // Assert
            Assert.IsTrue(testObj.Get.StartsWith(" "));
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

        [TestMethod]
        public void ItemInGenericListIsTrimmed()
        {
            // Arrange
            var testObj = new ObjectList { Name = " List Name " };
            testObj.Add(" Trim me ");

            // Act
            testObj.TrimStringProperties();

            // Assert
            Assert.AreEqual("List Name", testObj.Name);
            Assert.AreEqual("Trim me", testObj[0]);
        }

        [TestMethod]
        public void EmptyDictionaryObjectsAreIgnored()
        {
            // Arrange
            var testObj = new ParentOfDictionary { Name = " List Name " };

            // Act
            testObj.TrimStringProperties();

            // Assert - doesn't throw exception and regular stuff is trimmed
            Assert.AreEqual("List Name", testObj.Name);
        }

        [TestMethod]
        public void DictionaryObjectsAreTrimmed()
        {
            // Arrange
            var testObj = new ParentOfDictionary();
            testObj.Map.Add("1", " Bad String ");

            // Act
            testObj.TrimStringProperties();

            // Assert
            Assert.AreEqual("Bad String", testObj.Map.Values.First());
        }

        [TestMethod]
        public void ObjectsOfTypeStringAreTrimmed()
        {
            // Arrange
            var testObj = new ObjectProperties { Name = " List Name ", StringAsObject = " Trim me " };

            // Act
            testObj.TrimStringProperties();

            // Assert - doesn't throw exception and regular stuff is trimmed
            Assert.AreEqual("List Name", testObj.Name);
            Assert.AreEqual("Trim me", testObj.StringAsObject);
        }
    }
}
