#if NETCOREAPP2_0
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.StringLibrary.Pluralization;
using System.Globalization;

namespace Rhyous.StringLibrary.Tests.Pluralization
{
    [TestClass]
    public class IETFLanguageTagDictionaryTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void IETFLanguageTagDictionary_Constructor_Tests()
        {
            // Arrange
            var dict = IETFLanguageTagDictionary.Instance;

            // Act
            // Assert
            Assert.IsTrue(dict.TryGetValue("en", out IPluralizer pluralizer2));
            Assert.IsTrue(dict.TryGetValue("en-US", out IPluralizer pluralizer5));
        }
    }
}
#endif
