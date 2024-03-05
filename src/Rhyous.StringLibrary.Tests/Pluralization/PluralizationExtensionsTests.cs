
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.EasyCsv;
using Rhyous.StringLibrary.Pluralization;
using Rhyous.UnitTesting;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Rhyous.StringLibrary.Tests.Pluralization
{
    [TestClass]
    public class PluralizationExtensionsTests
    {

        // The below three tests will fail on non-english languages until a Pluralizer is written for that language.
        #region Default to the computers localization. 

        [TestMethod]
        [CsvTestDataSource(@"Data/IrregularEnglishNouns.csv")]
        public void PluralizationExtensions_Pluralize_Irregular_YourComputersLanguage_Test(Row<string> row)
        {
            // Arrange
            string noun = row[0];
            string expectedPlural = row[1];

            // Act
            var actualPlural = noun.Pluralize();

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [CsvTestDataSource(@"Data/RegularEnglishNouns.csv")]
        public void PluralizationExtensions_Pluralize_Regular_YourComputersLanguage_Test(Row<string> row)
        {
            // Arrange
            string noun = row[0];
            string expectedPlural = row[1];

            // Act
            var actualPlural = noun.Pluralize();

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }
        
        [TestMethod]
        [CsvTestDataSource(@"Data/AlphabetAndNumbers.csv")]
        public void PluralizationExtensions_Pluralize_AlphabetAndNumbers_YourComputersLanguage_Test(Row<string> row)
        {
            // Arrange
            string noun = row[0];
            string expectedPlural = row[1];

            // Act
            var actualPlural = noun.Pluralize();

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }
        #endregion

        #region en-US
        [TestMethod]
        [CsvTestDataSource(@"Data/IrregularEnglishNouns.csv")]
        public void PluralizationExtensions_Pluralize_Irregular_enUS_Test(Row<string> row)
        {
            // Arrange
            string noun = row[0];
            string expectedPlural = row[1];

            // Act
            var actualPlural = noun.Pluralize(null, new CultureInfo("en-US"));

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [CsvTestDataSource(@"Data/RegularEnglishNouns.csv")]
        public void PluralizationExtensions_Pluralize_Regular_enUS_Test(Row<string> row)
        {
            // Arrange
            string noun = row[0];
            string expectedPlural = row[1];

            // Act
            var actualPlural = noun.Pluralize(null, new CultureInfo("en-US"));

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [CsvTestDataSource(@"Data/AlphabetAndNumbers.csv")]
        public void PluralizationExtensions_Pluralize_AlphabetAndNumbers_enUS_Test(Row<string> row)
        {
            // Arrange
            string noun = row[0];
            string expectedPlural = row[1];

            // Act
            var actualPlural = noun.Pluralize(null, new CultureInfo("en-US"));

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [CsvTestDataSource(@"Data/CustomPluralizers.csv")]
        public void PluralizationExtensions_Pluralize_CustomAddition_enUS_Test(Row<string> row)
        {
            // Arrange
            string noun = row[0];
            string expectedPlural = row[1];

            IETFLanguageTagDictionary.Instance["en-US"].PluralizationDictionary.Add(noun, expectedPlural);

            // Act
            var actualPlural = noun.Pluralize();

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }
        #endregion

        #region en-GB
        [TestMethod]
        [CsvTestDataSource(@"Data/IrregularEnglishNouns.csv")]
        public void PluralizationExtensions_Pluralize_Irregular_enGB_Test(Row<string> row)
        {
            // Arrange
            string noun = row[0];
            string expectedPlural = row[1];

            // Act
            var actualPlural = noun.Pluralize(null, new CultureInfo("en-GB"));

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [CsvTestDataSource(@"Data/RegularEnglishNouns.csv")]
        public void PluralizationExtensions_Pluralize_Regular_enGB_Test(Row<string> row)
        {
            // Arrange
            string noun = row[0];
            string expectedPlural = row[1];

            // Act
            var actualPlural = noun.Pluralize(null, new CultureInfo("en-GB"));

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [CsvTestDataSource(@"Data/AlphabetAndNumbers.csv")]
        public void PluralizationExtensions_Pluralize_AlphabetAndNumbers_enGB_Test(Row<string> row)
        {
            // Arrange
            string noun = row[0];
            string expectedPlural = row[1];

            // Act
            var actualPlural = noun.Pluralize(null, new CultureInfo("en-GB"));

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [CsvTestDataSource(@"Data/CustomPluralizers.csv")]
        public void PluralizationExtensions_Pluralize_CustomAddition_enGB_Test(Row<string> row)
        {
            // Arrange
            string noun = row[0];
            string expectedPlural = row[1];

            IETFLanguageTagDictionary.Instance["en"].PluralizationDictionary.Add(noun, expectedPlural);

            // Act
            var actualPlural = noun.Pluralize();

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }
        #endregion

        #region Pluralize single word tests
        [TestMethod]
        public void PluralizationExtensions_Pluralize_OneWord_Test()
        {
            // Arrange
            string noun = "s";
            string expectedPlural = "s's";

            // Act
            var actualPlural = noun.Pluralize();

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        public void PluralizationExtensions_Pluralize_TwoLetterISOLanguageName_Test()
        {
            // Arrange
            string noun = "s";
            string expectedPlural = "s's";

            // Act
            var actualPlural = noun.Pluralize(null, new CultureInfo("en-GB"));

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [ExpectedException(typeof(LanguagePluralizerMissingException))]
        public void PluralizationExtensions_Pluralize_MissingPluralizerException_Test()
        {
            // Arrange
            string noun = "word";

            // Act
            // Assert
            var actualPlural = noun.Pluralize(null, new CultureInfo("es-ES"));
        }
        #endregion
    }
}
