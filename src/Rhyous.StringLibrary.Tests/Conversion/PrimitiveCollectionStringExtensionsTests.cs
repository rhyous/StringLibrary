using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.StringLibrary.Conversion;
using Rhyous.UnitTesting;
using System;
using System.Globalization;
using System.Linq;

namespace Rhyous.StringLibrary.Tests.Conversion
{
    [TestClass]
    public class PrimitiveCollectionStringExtensionsTests
    {
        [TestMethod]
        [StringIsNullEmptyOrWhitespace]
        public void PrimitiveCollectionStringExtensions_ToTypedEnumerable_long_StringNullEmptyOrWhitespace_ReturnsEmptyEnumerable(string s)
        {
            // Arrange
            // Act
            var result = s.ToTypedEnumerable<long>();

            // Assert
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void PrimitiveCollectionStringExtensions_ToTypedEnumerable_long_StringHasSingleItem_ReturnsTypedEnumerableWithSingleItem()
        {
            // Arrange
            var s = "27";

            // Act
            var result = s.ToTypedEnumerable<long>();

            // Assert
            Assert.AreEqual(27, result.First());
        }

        [TestMethod]
        public void PrimitiveCollectionStringExtensions_ToTypedEnumerable_long_StringHasMultipleItems_ReturnsTypedEnumerableWithMultipleItems()
        {
            // Arrange
            var s = "27,28,29";

            // Act
            var result = s.ToTypedList<long>();

            // Assert
            Assert.AreEqual(27, result[0]);
            Assert.AreEqual(28, result[1]);
            Assert.AreEqual(29, result[2]);
        }

        [TestMethod]
        public void PrimitiveCollectionStringExtensions_ToTypedEnumerable_long_StringHasMultipleItems_WithWhiteSpace_ReturnsTypedEnumerableWithMultipleItems()
        {
            // Arrange
            var s = " 27, 28 , 29 ";

            // Act
            var result = s.ToTypedList<long>();

            // Assert
            Assert.AreEqual(27, result[0]);
            Assert.AreEqual(28, result[1]);
            Assert.AreEqual(29, result[2]);
        }

        [TestMethod]
        public void PrimitiveCollectionStringExtensions_ToTypedEnumerable_long_StringHasMultipleItems_SeparateChangedToPipe_ReturnsTypedEnumerableWithMultipleItems()
        {
            // Arrange
            var s = "27|28|29";

            // Act
            var result = s.ToTypedList<long>("|");

            // Assert
            Assert.AreEqual(27, result[0]);
            Assert.AreEqual(28, result[1]);
            Assert.AreEqual(29, result[2]);
        }

        [TestMethod]
        public void PrimitiveCollectionStringExtensions_ToTypedEnumerable_Guid_StringHasMultipleItems_ReturnsTypedEnumerableWithMultipleItems()
        {
            // Arrange
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();
            var s = $"{guid1},{guid2},{guid3}";

            // Act
            var result = s.ToTypedList<Guid>();

            // Assert
            Assert.AreEqual(guid1, result[0]);
            Assert.AreEqual(guid2, result[1]);
            Assert.AreEqual(guid3, result[2]);
        }

        [TestMethod]
        public void PrimitiveCollectionStringExtensions_ToTypedEnumerable_Double_StringHasMultipleItems_ReturnsTypedEnumerableWithMultipleItems()
        {
            // Arrange
            var s = "27.1,28.2,29.3";

            // Act
            var result = s.ToTypedList<double>();

            // Assert
            Assert.AreEqual(27.1, result[0]);
            Assert.AreEqual(28.2, result[1]);
            Assert.AreEqual(29.3, result[2]);
        }

        /// <summary>
        /// In some cultures, the decimal separator is a comma, so this test ensures that the method can handle that.
        /// </summary>
        [TestMethod]
        public void PrimitiveCollectionStringExtensions_ToTypedEnumerable_Double_StringHasMultipleItems_CommaForDecimal_ReturnsTypedEnumerableWithMultipleItems()
        {
            // Arrange
            var s = "27,1|28,2|29,3";
            var cultureInfo = new CultureInfo("fr-FR"); // French culture uses comma as decimal separator

            // Act
            var result = s.ToTypedList<double>("|", cultureInfo);

            // Assert
            Assert.AreEqual(27.1, result[0]);
            Assert.AreEqual(28.2, result[1]);
            Assert.AreEqual(29.3, result[2]);
        }

        [TestMethod]
        public void PrimitiveCollectionStringExtensions_ToTypedEnumerable_Enum_StringHasMultipleItems_ReturnsTypedEnumerableWithMultipleItems()
        {
            // Arrange
            var s = "0,1,2";

            // Act
            var result = s.ToTypedList<MyTestEnum>();

            // Assert
            Assert.AreEqual(MyTestEnum.A, result[0]);
            Assert.AreEqual(MyTestEnum.B, result[1]);
            Assert.AreEqual(MyTestEnum.C, result[2]);
        }

        [TestMethod]
        public void PrimitiveCollectionStringExtensions_ToTypedEnumerable_EnumByName_StringHasMultipleItems_ReturnsTypedEnumerableWithMultipleItems()
        {
            // Arrange
            var s = "A,B,C";

            // Act
            var result = s.ToTypedList<MyTestEnum>();

            // Assert
            Assert.AreEqual(MyTestEnum.A, result[0]);
            Assert.AreEqual(MyTestEnum.B, result[1]);
            Assert.AreEqual(MyTestEnum.C, result[2]);
        }

        [TestMethod]
        public void PrimitiveCollectionStringExtensions_ToTypedEnumerable_EnumByCaseInsensitiveName_StringHasMultipleItems_ReturnsTypedEnumerableWithMultipleItems()
        {
            // Arrange
            var s = "a,b,c";

            // Act
            var result = s.ToTypedList<MyTestEnum>();

            // Assert
            Assert.AreEqual(MyTestEnum.A, result[0]);
            Assert.AreEqual(MyTestEnum.B, result[1]);
            Assert.AreEqual(MyTestEnum.C, result[2]);
        }
    }
    enum MyTestEnum { A, B, C }

}
