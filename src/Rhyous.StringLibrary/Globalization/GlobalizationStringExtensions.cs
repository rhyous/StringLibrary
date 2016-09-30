using System.Globalization;
using System.Linq;
using System.Text;

namespace Rhyous.StringLibrary.Diacritics
{
    public static class GlobalizationStringExtensions
    {
        /// <summary>
        /// Removes diacritics from a string.
        /// </summary>
        /// <param name="stIn"></param>
        /// <returns>string without diacitics</returns>
        public static string RemoveDiacritics(this string inString)
        {
            var strFormD = inString.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (char c in strFormD.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
            {
                sb.Append(c);
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }
    }
}
