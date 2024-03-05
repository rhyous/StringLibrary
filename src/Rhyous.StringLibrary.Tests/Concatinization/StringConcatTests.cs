using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;

namespace Rhyous.StringLibrary.Tests.Comparison
{
    [TestClass]
    public class StringConcatTests
    {
        public TestContext TestContext { get; set; }

        #region Argument null exception tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConcatinizationExtensions_NullThrows_Test()
        {
            StringConcat.WithSeparator('/', null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConcatinizationExtensions_EmptyThrows_Test()
        {
            StringConcat.WithSeparator('/', new string[] { });
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConcatinizationExtensions_ThreeString_FirstNull_Test()
        {
            string s1 = null;
            string s2 = "test2";
            var s3 = "test3";
            var separator = '/';

            StringConcat.WithSeparator(separator, new[] { s1, s2, s3 });
        }

        [TestMethod]

        [ExpectedException(typeof(ArgumentNullException))]
        public void ConcatinizationExtensions_ThreeString_MiddleNull_Test()
        {
            var s1 = "test1";
            string s2 = null;
            var s3 = "test3";
            var separator = '/';

            StringConcat.WithSeparator(separator, new[] { s1, s2, s3 });
        }
        #endregion

        [TestMethod]
        public void ConcatinizationExtensions_SingleString_NoSeparator_Test()
        {
            var s1 = "test";
            var actual = StringConcat.WithSeparator('/', new string[] { s1 });
            Assert.AreEqual(s1, actual);
        }

        [TestMethod]
        public void ConcatinizationExtensions_TwoStrings_Separated_Test()
        {
            var s1 = "test1";
            var s2 = "test2";
            var separator = '/';
            var expected = "test1/test2";
            var actual = StringConcat.WithSeparator(separator, new[] { s1, s2 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConcatinizationExtensions_ThreeStrings_Separated_Test()
        {
            var s1 = "test1";
            var s2 = "test2";
            var s3 = "test3";
            var separator = '/';
            var expected = "test1/test2/test3";
            var actual = StringConcat.WithSeparator(separator, new[] { s1, s2, s3 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConcatinizationExtensions_FrontSlashOnFirstStringNotTrimmed_Test()
        {
            var s1 = "/test1";
            var s2 = "test2";
            var s3 = "test3";
            var separator = '/';
            var expected = "/test1/test2/test3";
            var actual = StringConcat.WithSeparator(separator, new[] { s1, s2, s3 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConcatinizationExtensions_AlreadyHaveSeparatorBothSides_Test()
        {
            var s1 = "/test1/";
            var s2 = "/test2/";
            var s3 = "/test3/";
            var separator = '/';
            var expected = "/test1/test2/test3";
            var actual = StringConcat.WithSeparator(separator, new[] { s1, s2, s3 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConcatinizationExtensions_WhiteSpace_Test()
        {
            var s1 = "/test1 /";
            var s2 = " /test2/ ";
            var s3 = "/ test3/ \t";
            var separator = '/';
            var expected = "/test1/test2/test3";
            var actual = StringConcat.WithSeparator(separator, new[] { s1, s2, s3 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConcatinizationExtensions_WhiteSpaceMultipleSlashes_Test()
        {
            var s1 = "/test1 /   /";
            var s2 = " /test2/   /";
            var s3 = "/ test3/ \t/";
            var separator = '/';
            var expected = "/test1/test2/test3";
            var actual = StringConcat.WithSeparator(separator, new[] { s1, s2, s3 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        //       left, right, separator, expected, Notes                                  
        [DataRow("a", "b", '?', "a?b", "Left and right set")]
        [DataRow("a?", "b", '?', "a?b", "Left and right set. Left has separator")]
        [DataRow("a", "?b", '?', "a?b", "Left and right set. Right has separator")]
        [DataRow("a?", "?b", '?', "a?b", "Left and right set. Both have separator")]
        [DataRow("a", "b", '/', "a/b", "Left and right set. Neither has separator")]
        [DataRow("a/", "b", '/', "a/b", "Left and right set. Left has separator")]
        [DataRow("a", "/b", '/', "a/b", "Left and right set. Right has separator")]
        [DataRow("a/", "/b", '/', "a/b", "Left and right set. Both have separator")]
        [DataRow("a", " b ", '/', "a/b", "Left and right set. Right with junk whitespace")]
        [DataRow("a", " / b ", '/', "a/b", "Left and right set. Right with junk whitespace")]
        [DataRow(" a ", "b", '?', "a?b", "Left and right set. Left with junk whitespace")]
        [DataRow(" a ? ", "b", '?', "a?b", "Left and right set. Left with junk whitespace")]
        public void ConcatinizationExtensions_WithSeparator_Data_Test(string left, string right, char separator, string expected, string notes)
        {
            // Arrange
            // Act
            var actual = StringConcat.WithSeparator(separator, left, right);

            // Assert
            Assert.AreEqual(expected, actual, notes);
        }
    }
}