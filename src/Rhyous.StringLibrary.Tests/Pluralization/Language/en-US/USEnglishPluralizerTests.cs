﻿#if NET461
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.StringLibrary.Pluralization;


namespace Rhyous.StringLibrary.Tests.Pluralization
{
    [TestClass]
    public class USEnglishPluralizerTests
    {
        public TestContext TestContext { get; set; }

        #region Pluralize
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
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\CustomPluralizers.csv", "CustomPluralizers#csv", DataAccessMethod.Sequential)]
        public void USEnglishPluralizer_IsPlural_CustomAddition_Test()
        {
            // Arrange
            string noun = TestContext.DataRow["Noun"].ToString();
            string plural = TestContext.DataRow["ExpectedPlural"].ToString();
            var pluralizer = new USEnglishPluralizer();
            pluralizer.PluralizationDictionary.Add(noun, plural);

            // Act
            // Assert
            Assert.IsTrue(pluralizer.IsPlural(plural));
        }
        #endregion
    }
}
#endif