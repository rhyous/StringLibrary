#if NETCOREAPP2_0
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.StringLibrary.Pluralization;


namespace Rhyous.StringLibrary.Tests.Pluralization
{
    [TestClass]
    public class USEnglishPluralizerTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\IrregularEnglishNouns.csv", "IrregularEnglishNouns#csv", DataAccessMethod.Sequential)]
        public void USEnglishPluralizer_Pluralize_Irregular_Test()
        {
            // Arrange
            string noun = TestContext.DataRow["Noun"].ToString();
            string expectedPlural = TestContext.DataRow["ExpectedPlural"].ToString();

            // Act
            var actualPlural = new USEnglishPluralizer().Pluralize(noun);

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\RegularEnglishNouns.csv", "RegularEnglishNouns#csv", DataAccessMethod.Sequential)]
        public void USEnglishPluralizer_Pluralize_Regular_Test()
        {
            // Arrange
            string noun = TestContext.DataRow["Noun"].ToString();
            string expectedPlural = TestContext.DataRow["ExpectedPlural"].ToString();

            // Act
            var actualPlural = new USEnglishPluralizer().Pluralize(noun);

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\AlphabetAndNumbers.csv", "AlphabetAndNumbers#csv", DataAccessMethod.Sequential)]
        public void USEnglishPluralizer_Pluralize_AlphabetAndNumbers_Test()
        {
            // Arrange
            string noun = TestContext.DataRow["Noun"].ToString();
            string expectedPlural = TestContext.DataRow["ExpectedPlural"].ToString();

            // Act
            var actualPlural = new USEnglishPluralizer().Pluralize(noun);

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        public void USEnglishPluralizer_Pluralize_OneWord_Test()
        {
            // Arrange
            string noun = "Hero";
            string expectedPlural = "Heroes";

            // Act
            var actualPlural = new USEnglishPluralizer().Pluralize(noun);

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        public void USEnglishPluralizer_ApplyStandardPluralizationRules_Null_Test()
        {
            // Arrange
            string noun = null;
            string expectedPlural = "s's";

            // Act
            var actualPlural = new USEnglishPluralizer().ApplyStandardPluralizationRules(noun);

            // Assert
            Assert.IsNull(actualPlural);
        }

        [TestMethod]
        public void USEnglishPluralizer_ApplyStandardPluralizationRules_Empty_Test()
        {
            // Arrange
            string noun = "";

            // Act
            var actualPlural = new USEnglishPluralizer().ApplyStandardPluralizationRules(noun);

            // Assert
            Assert.AreEqual(noun, actualPlural);
        }

        [TestMethod]
        public void USEnglishPluralizer_ApplyStandardPluralizationRules_WhiteSpace_Test()
        {
            // Arrange
            string noun = "   ";

            // Act
            var actualPlural = new USEnglishPluralizer().ApplyStandardPluralizationRules(noun);

            // Assert
            Assert.AreEqual(noun, actualPlural);
        }
    }
}
#endif