#if NET461
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.StringLibrary.Pluralization;
using System.Globalization;

namespace Rhyous.StringLibrary.Tests.Pluralization
{
    [TestClass]
    public class PluralizationExtensionsTests
    {
        public TestContext TestContext { get; set; }

        // The below three tests will fail on non-english languages until a Pluralizer is written for that language.
        #region Default to the computers localization. 
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\IrregularEnglishNouns.csv", "IrregularEnglishNouns#csv", DataAccessMethod.Sequential)]
        public void PluralizationExtensions_Pluralize_Irregular_YourComputersLanguage_Test()
        {
            // Arrange
            string noun = TestContext.DataRow["Noun"].ToString();
            string expectedPlural = TestContext.DataRow["ExpectedPlural"].ToString();

            // Act
            var actualPlural = noun.Pluralize();

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\RegularEnglishNouns.csv", "RegularEnglishNouns#csv", DataAccessMethod.Sequential)]
        public void PluralizationExtensions_Pluralize_Regular_YourComputersLanguage_Test()
        {
            // Arrange
            string noun = TestContext.DataRow["Noun"].ToString();
            string expectedPlural = TestContext.DataRow["ExpectedPlural"].ToString();

            // Act
            var actualPlural = noun.Pluralize();

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\AlphabetAndNumbers.csv", "AlphabetAndNumbers#csv", DataAccessMethod.Sequential)]
        public void PluralizationExtensions_Pluralize_AlphabetAndNumbers_YourComputersLanguage_Test()
        {
            // Arrange
            string noun = TestContext.DataRow["Noun"].ToString();
            string expectedPlural = TestContext.DataRow["ExpectedPlural"].ToString();

            // Act
            var actualPlural = noun.Pluralize();

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\CustomPluralizers.csv", "CustomPluralizers#csv", DataAccessMethod.Sequential)]
        public void PluralizationExtensions_Pluralize_CustomAddition_YourComputersLanguage_Test()
        {
            // Arrange
            string noun = TestContext.DataRow["Noun"].ToString();
            string expectedPlural = TestContext.DataRow["ExpectedPlural"].ToString();

            IETFLanguageTagDictionary.Instance[CultureInfo.CurrentCulture.TwoLetterISOLanguageName].PluralizationDictionary.Add(noun, expectedPlural);

            // Act
            var actualPlural = noun.Pluralize();

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }
        #endregion

        #region en-US
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\IrregularEnglishNouns.csv", "IrregularEnglishNouns#csv", DataAccessMethod.Sequential)]
        public void PluralizationExtensions_Pluralize_Irregular_enUS_Test()
        {
            // Arrange
            string noun = TestContext.DataRow["Noun"].ToString();
            string expectedPlural = TestContext.DataRow["ExpectedPlural"].ToString();

            // Act
            var actualPlural = noun.Pluralize(null, new CultureInfo("en-US"));

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\RegularEnglishNouns.csv", "RegularEnglishNouns#csv", DataAccessMethod.Sequential)]
        public void PluralizationExtensions_Pluralize_Regular_enUS_Test()
        {
            // Arrange
            string noun = TestContext.DataRow["Noun"].ToString();
            string expectedPlural = TestContext.DataRow["ExpectedPlural"].ToString();

            // Act
            var actualPlural = noun.Pluralize(null, new CultureInfo("en-US"));

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\AlphabetAndNumbers.csv", "AlphabetAndNumbers#csv", DataAccessMethod.Sequential)]
        public void PluralizationExtensions_Pluralize_AlphabetAndNumbers_enUS_Test()
        {
            // Arrange
            string noun = TestContext.DataRow["Noun"].ToString();
            string expectedPlural = TestContext.DataRow["ExpectedPlural"].ToString();

            // Act
            var actualPlural = noun.Pluralize(null, new CultureInfo("en-US"));

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\CustomPluralizers.csv", "CustomPluralizers#csv", DataAccessMethod.Sequential)]
        public void PluralizationExtensions_Pluralize_CustomAddition_enUS_Test()
        {
            // Arrange
            string noun = TestContext.DataRow["Noun"].ToString();
            string expectedPlural = TestContext.DataRow["ExpectedPlural"].ToString();

            IETFLanguageTagDictionary.Instance["en-US"].PluralizationDictionary.Add(noun, expectedPlural);

            // Act
            var actualPlural = noun.Pluralize();

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }
        #endregion

        #region en-GB
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\IrregularEnglishNouns.csv", "IrregularEnglishNouns#csv", DataAccessMethod.Sequential)]
        public void PluralizationExtensions_Pluralize_Irregular_enGB_Test()
        {
            // Arrange
            string noun = TestContext.DataRow["Noun"].ToString();
            string expectedPlural = TestContext.DataRow["ExpectedPlural"].ToString();

            // Act
            var actualPlural = noun.Pluralize(null, new CultureInfo("en-GB"));

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\RegularEnglishNouns.csv", "RegularEnglishNouns#csv", DataAccessMethod.Sequential)]
        public void PluralizationExtensions_Pluralize_Regular_enGB_Test()
        {
            // Arrange
            string noun = TestContext.DataRow["Noun"].ToString();
            string expectedPlural = TestContext.DataRow["ExpectedPlural"].ToString();

            // Act
            var actualPlural = noun.Pluralize(null, new CultureInfo("en-GB"));

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\AlphabetAndNumbers.csv", "AlphabetAndNumbers#csv", DataAccessMethod.Sequential)]
        public void PluralizationExtensions_Pluralize_AlphabetAndNumbers_enGB_Test()
        {
            // Arrange
            string noun = TestContext.DataRow["Noun"].ToString();
            string expectedPlural = TestContext.DataRow["ExpectedPlural"].ToString();

            // Act
            var actualPlural = noun.Pluralize(null, new CultureInfo("en-GB"));

            // Assert
            Assert.AreEqual(expectedPlural, actualPlural);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\CustomPluralizers.csv", "CustomPluralizers#csv", DataAccessMethod.Sequential)]
        public void PluralizationExtensions_Pluralize_CustomAddition_enGB_Test()
        {
            // Arrange
            string noun = TestContext.DataRow["Noun"].ToString();
            string expectedPlural = TestContext.DataRow["ExpectedPlural"].ToString();

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
#endif
