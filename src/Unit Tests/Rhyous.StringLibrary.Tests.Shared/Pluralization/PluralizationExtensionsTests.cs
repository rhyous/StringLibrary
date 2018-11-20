#if NETCOREAPP2_0
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.StringLibrary.Pluralization;
using System.Globalization;

namespace Rhyous.StringLibrary.Tests.Pluralization
{
    [TestClass]
    public class PluralizationExtensionsTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\IrregularEnglishNouns.csv", "IrregularEnglishNouns#csv", DataAccessMethod.Sequential)]
        public void PluralizationExtensions_Pluralize_Irregular_Test()
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
        public void PluralizationExtensions_Pluralize_Regular_Test()
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
        public void PluralizationExtensions_Pluralize_AlphabetAndNumbers_Test()
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
    }
}
#endif
