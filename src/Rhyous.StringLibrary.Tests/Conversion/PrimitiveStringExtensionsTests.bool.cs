using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.UnitTesting;

namespace Rhyous.StringLibrary.Tests.Conversion
{
    [TestClass]
    public class PrimitiveStringExtensionsBoolTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [PrimitiveList("False", "false", "FALSE", "0", "asdfasdf", "0", "", "  ", null)]
        public void PrimitiveStringExtensions_To_Bool_False_Test(string booleanString)
        {
            Assert.IsFalse(booleanString.To<bool>());
        }

        /// <summary>Strings that should return true.</summary>
        /// <param name="booleanString"></param>
        [TestMethod]
        [PrimitiveList("True", "True", "True", "1")]
        public void PrimitiveStringExtensions_To_Bool_True_Test(string booleanString)
        {
            Assert.IsTrue(booleanString.To<bool>());
        }

        [TestMethod]
        [PrimitiveList("", null)]
        public void PrimitiveStringExtensions_To_Bool_OverrideDefaultValue_Test(string booleanString)
        {
            Assert.IsTrue(booleanString.To<bool>(true));
        }
    }
}