using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.EasyCsv;
using Rhyous.StringLibrary.Pluralization;
using Rhyous.UnitTesting;


namespace Rhyous.StringLibrary.Tests.Pluralization
{
    [TestClass]
    public class USEnglishPluralizerTests
    {
        #region Pluralize
        [TestMethod]
        [CsvTestDataSource(@"Data/IrregularEnglishNouns.csv")]
        public void USEnglishPluralizer_Pluralize_Irregular_Test(Row<string> row)
        {
            // Arrange
            string noun = row[0];
            string expectedPlural = row[1];

            // Act
            var actualPlural = new USEnglishPluralizer().Pluralize(noun);

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [CsvTestDataSource(@"Data/RegularEnglishNouns.csv")]
        public void USEnglishPluralizer_Pluralize_Regular_Test(Row<string> row)
        {
            // Arrange
            string noun = row[0];
            string expectedPlural = row[1];

            // Act
            var actualPlural = new USEnglishPluralizer().Pluralize(noun);

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }
        
        [TestMethod]
        [CsvTestDataSource(@"Data/AlphabetAndNumbers.csv")]
        public void USEnglishPluralizer_Pluralize_AlphabetAndNumbers_Test(Row<string> row)
        {
            // Arrange
            string noun = row[0];
            string expectedPlural = row[1];
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
        #endregion

        #region ApplyStandardPluralizationRules

        [TestMethod]
        public void USEnglishPluralizer_ApplyStandardPluralizationRules_Null_Test()
        {
            // Arrange
            string noun = null;

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
        #endregion

        #region IsPlural

        [CsvTestDataSource(@"Data/CustomPluralizers.csv")]
        public void USEnglishPluralizer_IsPlural_CustomAddition_Test(Row<string> row)
        {
            // Arrange
            string noun = row[0];
            string plural = row[1];
            var pluralizer = new USEnglishPluralizer();
            pluralizer.PluralizationDictionary.Add(noun, plural);

            // Act
            // Assert
            Assert.IsTrue(pluralizer.IsPlural(plural));
        }
        #endregion
    }
}