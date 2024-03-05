using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Rhyous.StringLibrary.Tests
{
    [TestClass]
    public class CryptoRandomStringTests
    {
        [TestMethod]
        public void TestTestThatCorrectCharacterCountIsReturned()
        {
            const int length = 100;
            var randomString = CryptoRandomString.GetCryptoRandomBase95String(length);
            Assert.AreEqual(length, randomString.Length);
        }

        [TestMethod]
        public void TestAllCharactersAreUsed()
        {
            const int length = 1000000;
            var randomString = CryptoRandomString.GetCryptoRandomBase95String(length);
            for (int i = 32; i < 126; i++)
            {
                char c = (char)i;
                Assert.IsTrue(randomString.Contains(c.ToString()));
            }
        }

        [TestMethod]
        [TestCategory("Performance")]
        public void TestPerformanceTenMillionCharacters()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            const int length = 1000000;
            CryptoRandomString.GetCryptoRandomBase95String(length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 1000);
        } // Elapsed Milliseconds 320 (it fluxuated a few milliseconds each run) 

        [TestMethod]
        [DoNotParallelize]
        [TestCategory("Performance")]
        public void TestPerformanceLoop()
        {
            Stopwatch watch = new Stopwatch();
            const int length = 16;
            watch.Start();
            for (int i = 0; i < 100000; i++)
            {
                CryptoRandomString.GetCryptoRandomBase95String(length);
            }
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 1000);
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void TestDistributionInTenMillionCharacters()
        {
            const int length = 1000000;
            const int distibution = length / 95;
            int[] margins = new int[9500];
            Parallel.For(0, 100, j =>
            {
                var randomString = CryptoRandomString.GetCryptoRandomBase95String(length);
                for (int i = 32; i < 127; i++)
                {
                    int count = CountInstancesOfChar(randomString, (char)i);
                    margins[(j * 95) + i - 32] = count;
                }
            });
            Assert.IsTrue(Math.Abs(margins.Average() - distibution) < .5);
        }

        private int CountInstancesOfChar(string str, char c)
        {
            int count = 0;
            char[] strArray = str.ToCharArray();
            int length = str.Length;
            for (int n = length - 1; n >= 0; n--)
            {
                if (strArray[n] == c)
                    count++;
            }
            return count;
        }

        [TestMethod]
        public void GetCryptoRandomAlphaNumericStringTest()
        {
            const int length = 100000;
            var randomString = CryptoRandomString.GetCryptoRandomAlphaNumericString(length);
            foreach (char c in randomString)
            {
                Assert.IsTrue(CryptoRandomString.AlphaNumeric.Contains(c));
            }
        }
    }
}
