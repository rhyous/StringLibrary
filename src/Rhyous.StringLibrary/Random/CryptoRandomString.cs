using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// A class that creates a Crypto random string using RNGCryptoServiceProvider.
    /// </summary>
    public class CryptoRandomString
    {
        /// <summary>A list of lowercase letters.</summary>
        public const string LowerCase = "abcdefghijklmnopqrstuvwxyz";
        /// <summary>A list of uppercase letters.</summary>
        public const string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        /// <summary>A list of numbers.</summary>
        public const string Numbers = "0123456789";
        /// <summary>A combination list of upper and lower case letters and numbers..</summary>
        public const string AlphaNumeric = LowerCase + UpperCase + Numbers;

        /// <summary>
        /// A list of non-ambiguous alphanumeric numbers. For example, it might be hard to tell apart O, the letter, and 0, a zero, so those are excluded.
        /// Excluded letters and numbers: 0, O, 1, l.
        /// </summary>
        public static string AlphaNumericNonAmbigous
        {
            get { return _AlphaNumericNonAmbiguous ?? (_AlphaNumericNonAmbiguous = new Regex("0|O|1|l").Replace(LowerCase + UpperCase + Numbers, "")); }
        } private static string _AlphaNumericNonAmbiguous;

        /// <summary>
        /// Gets a crypto random string of the specified length.
        /// </summary>
        /// <param name="length">The length of the random string.</param>
        /// <returns></returns>
        public static string GetCryptoRandomBase64String(int length)
        {
            var buffer = new byte[length];
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetNonZeroBytes(buffer);
            }
            return Convert.ToBase64String(buffer);
        }
        
        /// <summary>
        /// Any character from ASCII decimal 32 (space), including alphanumeric and punctuation, 
        /// to ASCII decimal 126 (~). This represents all characters that an english keyboard 
        /// could easily type.
        /// </summary>
        /// <param name="length">The length of the random string.</param>
        /// <returns>A random string made up of Alphanumeric characters.</returns>
        public static string GetCryptoRandomBase95String(int length)
        {
            return GetCryptoRandomBaseNString(length, 95);
        }

        /// <summary>
        /// Gets a crypto random string using base n.
        /// </summary>
        /// <param name="length">The length of the random string.</param>
        /// <param name="baseN">The number of ASCII characters to use.</param>
        /// <param name="asciiOffset">This determines which character in ASCII to start with.</param>
        /// <param name="allowedCharacters">Characters that are allowed. If a random character is chosen and isn't allowed, a new random character is chosen.</param>
        /// <returns>A random string.</returns>
        public static string GetCryptoRandomBaseNString(int length, byte baseN, int asciiOffset = 32, char[] allowedCharacters = null)
        {
            var buffer = new byte[length];
            var builder = new StringBuilder();

            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetBytes(buffer);
                foreach (var b in buffer)
                {
                    var tmpbuff = new byte[] { b };
                    int max = (baseN * (256 / baseN)) - 1; // minus 1 because we start at 0
                    while (tmpbuff[0] > max)
                    {
                        rngCryptoServiceProvider.GetBytes(tmpbuff);
                    }
                    var singleChar = (allowedCharacters == null)
                        ? ByteToBaseNChar(tmpbuff[0], baseN, asciiOffset) // Start at ascii 32 (space) by default
                        : ByteToAllowedCharacter(tmpbuff[0], allowedCharacters);
                    builder.Append(singleChar);
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Gets a random string made up of Alphanumeric characters.
        /// </summary>
        /// <param name="length">The length of the random string.</param>
        /// <param name="removeAmbiguous">By default this is false. If true, ambigous letters are excluded.</param>
        /// <returns>A random string made up of Alphanumeric characters.</returns>
        public static string GetCryptoRandomAlphaNumericString(int length, bool removeAmbiguous = false)
        {
            return GetCryptoRandomBaseNString(length, (byte)AlphaNumeric.Length, 0, removeAmbiguous
                ? AlphaNumeric.ToCharArray()
                : AlphaNumericNonAmbigous.ToCharArray());
        }

        /// <summary>
        /// Converts a byte to a character of base N.
        /// </summary>
        /// <param name="b">A byte.</param>
        /// <param name="baseN">The number of ASCII characters to use.</param>
        /// <param name="asciiOffset">This determines which character in ASCII to start with.</param>
        /// <returns></returns>
        public static char ByteToBaseNChar(byte b, int baseN, int asciiOffset)
        {
            return (char)(b % baseN + asciiOffset);
        }

        /// <summary>
        /// Converts a byte to an allowed character.
        /// </summary>
        /// <param name="b">A byte.</param>
        /// <param name="allowedCharacters">a list of allowed characters.</param>
        /// <returns></returns>
        public static char ByteToAllowedCharacter(byte b, char[] allowedCharacters)
        {
            return allowedCharacters[(b % allowedCharacters.Length)];
        }
    }
}
