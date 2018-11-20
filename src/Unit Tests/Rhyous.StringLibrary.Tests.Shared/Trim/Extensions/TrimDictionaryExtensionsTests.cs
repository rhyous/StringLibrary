using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhyous.StringLibrary.Tests
{
    [TestClass]
    public class TrimDictionaryExtensionsTests
    {
        [TestMethod]
        public void TrimDictionaryExtensions_NullDictionary_Test()
        {
            // Arrange
            Dictionary<string, string> dict = null;

            // Act
            dict.TrimDictionary();

            // Assert
            Assert.IsNull(dict);
        }

        [TestMethod]
        public void TrimDictionaryExtensions_ListStringDictionary_Test()
        {
            // Arrange
            Dictionary<int, List<string>> dict = new Dictionary<int, List<string>> { { 1, new List<string> { " A ", "B " } }, { 2, new List<string> { " C ", " D" } } };

            // Act
            dict.TrimDictionary();

            // Assert
            CollectionAssert.AreEqual(new[] { "A", "B" }, dict.Values.First());
            CollectionAssert.AreEqual(new[] { "C", "D" }, dict.Values.Skip(1).First());
        }
    }
}
