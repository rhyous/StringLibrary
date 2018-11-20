using System.Collections.Generic;
using System.Globalization;

namespace Rhyous.StringLibrary.Pluralization
{
    public static class PluralizationExtensions
    {
        public static string Pluralize(this string noun, IDictionary<string, string> customPluralizationDictionary = null, CultureInfo culture = null)
        {
            culture = culture ?? CultureInfo.CurrentCulture;
            if (IETFLanguageTagDictionary.Instance.TryGetValue(culture.IetfLanguageTag, out IPluralizer pluralizer) // Exact culture
             || IETFLanguageTagDictionary.Instance.TryGetValue(culture.TwoLetterISOLanguageName, out pluralizer)) // Base culture
                return pluralizer.Pluralize(noun, customPluralizationDictionary);
            throw new LanguagePluralizerMissingException(culture.IetfLanguageTag);
        }
    }
}