using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rhyous.StringLibrary.Tests.Shared.Globalization
{
    [TestClass]
    public class GlobalizationStringExtensionsTests
    {
        [TestMethod]
        public void GlobalizationStringExtensions_RemoveDiacritics_Test()
        {
            // Arrange
            var s = "èéüïâîôñç";
            var expected = "eeuiaionc";

            // Act
            var actual = s.RemoveDiacritics();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
