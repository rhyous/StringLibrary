using System;
using System.Linq;
using System.Text;

namespace Rhyous.StringLibrary
{
    public static class StringCapitalizationExtensions
    {
        public static string CapitalizeFirstLetter(this string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return word;
            char[] charArray = word.ToCharArray();
            charArray[0] = char.ToUpper(charArray[0]);
            return new string(charArray);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wordToChange"></param>
        /// <param name="wordToMatch"></param>
        /// <returns></returns>
        /// <remarks>This is an algorithm, which allows it to break the 10/100 rules.</remarks>
        public static string MatchCapitalization(this string wordToChange, string wordToMatch)
        {
            // Supported Capitalization scenarios
            // [X] All uppercase
            // [X] All lowercase
            // [X] First letter uppercase
            // [X] More than one letter uppercase.
            // [x] Different length strings (such as capitalizations)
            if (wordToMatch.All(c => char.IsUpper(c))) // All upper case
                return wordToChange.ToUpper();
            if (wordToMatch.All(c => char.IsLower(c))) // All upper case
                return wordToChange.ToLower();
            var builder = new StringBuilder();
            for (int i = 0, j = 0; i < wordToChange.Length;)
            {
                var c1 = wordToChange[i];
                if (j >= wordToMatch.Length)
                {
                    builder.Append(c1);
                    i++;
                    continue;
                }
                var c2 = wordToMatch[j];
                if (c1 == c2)
                { 
                    builder.Append(c1);
                    i++; j++;
                    continue;
                }
                if (c1.ToString().Equals(c2.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    char c = i < wordToMatch.Length
                           ? (char.IsUpper(wordToMatch[i])
                                  ? char.ToUpper(wordToChange[i])
                                  : char.ToLower(wordToChange[i]))
                           : wordToChange[i];
                    builder.Append(c);
                    i++; j++;
                    continue;
                }
                var compare = wordToMatch.Length.CompareTo(wordToChange.Length);
                switch (compare)
                {
                    case 1: // Greater
                        j++;
                        break;
                    case 0: // Equal
                        builder.Append(c1);
                        i++; j++;
                        break;
                    case -1: // Less
                        builder.Append(c1);
                        i++;
                        break;
                }
            }
            return builder.ToString();
        }
    }
}