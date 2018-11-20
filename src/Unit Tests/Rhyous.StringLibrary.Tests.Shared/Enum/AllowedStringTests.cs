using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhyous.StringLibrary.Tests
{
    [TestClass]
    public class AllowedStringTests
    {
        [TestMethod]
        public void AllowedString_Constructor_Test()
        {
            // Arrange
            var vowels = new[] { "A", "E", "I", "O", "U" };
            var defaultValue = "A";
            // Act
            var allowedString = new AllowedString(vowels, defaultValue);

            // Assert
            Assert.AreEqual(defaultValue, allowedString.DefaultValue);
            CollectionAssert.AreEqual(vowels, allowedString.AllowedValues.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AllowedString_Constructor_NullAllowedValues_Test()
        {
            // Arrange
            IEnumerable<string> vowels = null;
            var defaultValue = "A";

            // Act
            // Assert
            new AllowedString(vowels, defaultValue);
        }

        [TestMethod]
        public void AllowedString_Constructor_NullDefaultValue_IsFirstAllowedValue_Test()
        {
            // Arrange
            var vowels = new[] { "A", "E", "I", "O", "U" };
            string defaultValue = "A";

            // Act
            var allowedString = new AllowedString(vowels);

            // Assert
            Assert.AreEqual(defaultValue, allowedString.DefaultValue);
            CollectionAssert.AreEqual(vowels, allowedString.AllowedValues.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(ValueNotAllowedException))]
        public void AllowedString_SetValue_Allowed_Test()
        {
            // Arrange
            var vowels = new[] { "A", "E", "I", "O", "U" };
            var defaultValue = "A";
            var allowedString = new AllowedString(vowels, defaultValue);

            // Act
            allowedString.Value = "I";

            // Assert
            Assert.AreEqual("I", allowedString.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ValueNotAllowedException))]
        public void AllowedString_SetValue_NotAllowed_Test()
        {
            // Arrange
            var vowels = new[] { "A", "E", "I", "O", "U" };
            var defaultValue = "A";
            var allowedString = new AllowedString(vowels, defaultValue);

            // Act
            // Assert
            allowedString.Value = "B";
        }
    }
}
