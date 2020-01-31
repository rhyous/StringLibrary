using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.StringLibrary.Tests
{
    [TestClass]
    public class TrimPropertiesTests
    {
        [TestMethod]
        public void TrimObjectExtensions_NullObjectDoesNotThrowException()
        {
            // Arrange
            TestObject testObj = null;

            // Act
            testObj.TrimStringProperties();

            // Assert 
            // Nothing to assert. If an exception is not thrown, that is enough.
        }

        [TestMethod]
        public void TrimObjectExtensions_RootObjectIsTrimmed()
        {
            // Arrange
            var testObj = new TestObject { Id = 1, Value = " Blah    String1 ", Sub = new SubTestObject { Id = 2, SubValue = "  Blah\tString2 " } };
            const string expected = "Blah String1";

            // Act
            testObj.TrimStringProperties();

            // Assert
            Assert.AreEqual(expected, testObj.Value);
        }

        [TestMethod]
        public void TrimObjectExtensions_SubObjectIsTrimmed()
        {
            // Arrange
            var testObj = new TestObject { Id = 1, Value = " Blah   String ", Sub = new SubTestObject { Id = 2, SubValue = "  Blah\tString2 " } };
            const string expected = "Blah String2";

            // Act
            testObj.TrimStringProperties();

            // Assert
            Assert.AreEqual(expected, testObj.Sub.SubValue);
        }

        [TestMethod]
        public void TrimObjectExtensions_GetterOnlyPropertiesAreIgnored()
        {
            // Arrange
            var testObj = new Getter();

            // Act
            testObj.TrimStringProperties();

            // Assert
            Assert.IsTrue(testObj.Get.StartsWith(" "));
        }

        [TestMethod]
        public void TrimObjectExtensions_ItemsInArrayAreTrimmed()
        {
            // Arrange
            var testObj = new string[] { " Trim me ", " Trim me \t too. " };

            // Act
            testObj.TrimStringProperties();

            // Assert
            Assert.AreEqual("Trim me", testObj[0]);
            Assert.AreEqual("Trim me too.", testObj[1]);
        }

        [TestMethod]
        public void TrimObjectExtensions_ItemsInArrayPropertyAreTrimmed()
        {
            // Arrange
            var testObj = new ContainsStringArray { Name = " Array Name " };
            testObj.Array = new string[] { " Trim me " };

            // Act
            testObj.TrimStringProperties();

            // Assert
            Assert.AreEqual("Array Name", testObj.Name);
            Assert.AreEqual("Trim me", testObj.Array[0]);
        }

        [TestMethod]
        public void TrimObjectExtensions_StringPropertyOfGenericListIsTrimmed()
        {
            // Arrange
            var testObj = new ObjectList { Name = " List Name " };

            // Act
            testObj.TrimStringProperties();

            // Assert - doesn't throw exception and regular stuff is trimmed
            Assert.AreEqual("List Name", testObj.Name);
        }

        [TestMethod]
        public void TrimObjectExtensions_ItemInGenericListIsTrimmed()
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
        public void TrimObjectExtensions_EmptyDictionaryObjectsAreIgnored()
        {
            // Arrange
            var testObj = new ParentOfDictionary { Name = " List Name " };

            // Act
            testObj.TrimStringProperties();

            // Assert - doesn't throw exception and regular stuff is trimmed
            Assert.AreEqual("List Name", testObj.Name);
        }

        [TestMethod]
        public void TrimObjectExtensions_DictionaryObjectsAreTrimmed()
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
        public void TrimObjectExtensions_ObjectsOfTypeStringAreTrimmed()
        {
            // Arrange
            var testObj = new ObjectProperties { Name = " List Name ", StringAsObject = " Trim me " };

            // Act
            testObj.TrimStringProperties();

            // Assert - doesn't throw exception and regular stuff is trimmed
            Assert.AreEqual("List Name", testObj.Name);
            Assert.AreEqual("Trim me", testObj.StringAsObject);
        }

        [TestMethod]
        public void TrimObjectExtensions_ObjectsWithReadonlyPropertiesAreNotTrimmed()
        {
            // Arrange            
            var testObj = new Tuple<string, string>(" Name1 ", " val1 ");

            // Act
            testObj.TrimStringProperties();

            // In a Tuple, Item1 and Item2 are readonly and immutable and cannot change.

            // Assert - doesn't throw exception and regular stuff is trimmed
            Assert.AreEqual(" Name1 ", testObj.Item1);
            Assert.AreEqual(" val1 ", testObj.Item2);
        }

        [TestMethod]
        public void TrimObjectExtensions_GenericObjectsAreTrimmed()
        {
            // Arrange            
            var testObj = new TestObjectGeneric<string> { Value = " Trim me. ", Sub = new SubTestObjectGeneric<string> { SubValue = " Trim me, too. " } };

            // Act
            testObj.TrimStringProperties();

            // In a Tuple, Item1 and Item2 are readonly and immutable and cannot change.

            // Assert - doesn't throw exception and regular stuff is trimmed
            Assert.AreEqual("Trim me.", testObj.Value);
            Assert.AreEqual("Trim me, too.", testObj.Sub.SubValue);
        }

        [TestMethod]
        public void TrimObjectExtensions_TrimDoesNotThrowIfSetThrows_String()
        {
            // Arrange            
            var testObj = new ObjectThatThrowsOnSet<string>(" A value in need of trimming. ");

            // Act
            testObj.TrimStringProperties();

            // In a Tuple, Item1 and Item2 are readonly and immutable and cannot change.

            // Assert - doesn't throw exception and regular stuff is trimmed
            Assert.AreEqual(" A value in need of trimming. ", testObj.Value);
        }

        [TestMethod]
        public void TrimObjectExtensions_TrimDoesNotThrowIfGetThrows_String()
        {
            // Arrange            
            var testObj = new ObjectThatThrowsOnGet<string>(" A value in need of trimming. ");

            // Act & Assert (we are just testing that an exception is not thrown here.
            testObj.TrimStringProperties();
        }

        [TestMethod]
        public void TrimObjectExtensions_TrimDoesNotThrowIfSetThrows_Object()
        {
            // Arrange            
            var testObj = new ObjectThatThrowsOnSet<object>(" A value in need of trimming. ");

            // Act
            testObj.TrimStringProperties();

            // In a Tuple, Item1 and Item2 are readonly and immutable and cannot change.

            // Assert - doesn't throw exception and regular stuff is trimmed
            Assert.AreEqual(" A value in need of trimming. ", testObj.Value);
        }

        [TestMethod]
        public void TrimObjectExtensions_TrimDoesNotThrowIfGetThrows_Object()
        {
            // Arrange            
            var testObj = new ObjectThatThrowsOnGet<object>(" A value in need of trimming. ");

            // Act & Assert (we are just testing that an exception is not thrown here.
            testObj.TrimStringProperties();
        }

        [TestMethod]
        public void TrimObjectExtensions_TrimDoesNotThrowIfSetThrows_ComplexObject()
        {
            // Arrange            
            var testObj = new TestObject { Value = " A value in need of trimming. " };
            var testObjThatThrows = new ObjectThatThrowsOnSet<TestObject>(testObj);

            // Act
            testObjThatThrows.TrimStringProperties();

            // In a Tuple, Item1 and Item2 are readonly and immutable and cannot change.

            // Assert - Set on Complex object, shouldn't happen, so trimming should work on child object.
            Assert.AreEqual("A value in need of trimming.", testObj.Value);
        }

        [TestMethod]
        public void TrimObjectExtensions_TrimDoesNotThrowIfGetThrows_ComplexObject()
        {
            // Arrange            
            var testObj = new TestObject { Value = " A value in need of trimming. ", Sub = new SubTestObject { SubValue = " A sub value in need of trimming. " } };
            var testObjThatThrows = new ObjectThatThrowsOnGet<TestObject>(testObj);

            // Act & Assert (we are just testing that an exception is not thrown here.
            testObjThatThrows.TrimStringProperties();

            // Assert
            Assert.AreEqual(" A value in need of trimming. ", testObj.Value);
            Assert.AreEqual(" A sub value in need of trimming. ", testObj.Sub.SubValue);
        }

        [TestMethod]
        public void TrimObjectExtensions_TrimDoesNotThrowIfSetThrows_Collection()
        {
            // Arrange            
            var testObj = new TestObject { Value = " A value in need of trimming. " };
            var testObjThatThrows = new ObjectThatThrowsOnSet<List<TestObject>>(new List<TestObject> { testObj });

            // Act
            testObjThatThrows.TrimStringProperties();

            // In a Tuple, Item1 and Item2 are readonly and immutable and cannot change.

            // Assert - Set on Complex object, shouldn't happen, so trimming should work on child object.
            Assert.AreEqual("A value in need of trimming.", testObj.Value);
        }

        [TestMethod]
        public void TrimObjectExtensions_TrimDoesNotThrowIfGetThrows_Collection()
        {  
            // Arrange            
            var testObj = new TestObject { Value = " A value in need of trimming. ", Sub = new SubTestObject { SubValue = " A sub value in need of trimming. " } };
            var testObjThatThrows = new ObjectThatThrowsOnGet<List<TestObject>>(new List<TestObject> { testObj });

            // Act & Assert (we are just testing that an exception is not thrown here.
            testObjThatThrows.TrimStringProperties();

            // Assert
            Assert.AreEqual(" A value in need of trimming. ", testObj.Value);
            Assert.AreEqual(" A sub value in need of trimming. ", testObj.Sub.SubValue);
        }
    }
}
