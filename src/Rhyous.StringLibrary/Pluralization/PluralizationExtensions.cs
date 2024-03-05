using System.Collections.Generic;
using System.Globalization;

namespace Rhyous.StringLibrary.Pluralization
{
    /// <summary>
    /// An extensions class that provides pluralizations methods to string.
    /// </summary>
    public static class PluralizationExtensions
    {
        /// <summary>
        /// A method to pluralize a word.
        /// </summary>
        /// <param name="noun">A word to pluralize.</param>
        /// <param name="customPluralizationDictionary">Optional. A custom pluralization dictionary.</param>
        /// <param name="culture">Optional. The culture. If none is applied the running culture is used.</param>
        /// <returns>The word pluralized.</returns>
        public static string Pluralize(this string noun, IDictionary<string, string> customPluralizationDictionary = null, CultureInfo culture = null)
        {
            string plural = null;
            culture = culture ?? CultureInfo.CurrentCulture ?? CultureInfo.GetCultureInfo("en-US");
            // We need to check the specific and less specific dictionaries for a custom plural before applying standard pluralization.
            IPluralizer[] pluralizers = new IPluralizer[2];
            plural = noun.PluralizeByCustomDictionaries(customPluralizationDictionary, culture, pluralizers);
            if (!string.IsNullOrWhiteSpace(plural))
                return plural;

            // Apply custom pluralization
            foreach (var pluralizer in pluralizers)
            {
                if (pluralizer != null)
                    return pluralizer.Pluralize(noun, customPluralizationDictionary);
            }

            // No pluralizer for either the 5- or 2-letter Iso tags.
            throw new LanguagePluralizerMissingException(culture.IetfLanguageTag);
        }

        private static string PluralizeByCustomDictionaries(this string noun, IDictionary<string, string> customPluralizationDictionary, CultureInfo culture, IPluralizer[] pluralizers)
        {
            var ietfIsoCodes = new[] { culture.IetfLanguageTag, culture.TwoLetterISOLanguageName };
            for (int i = 0; i < 2; i++)
            {
                var ietfIsoCode = ietfIsoCodes[i];
                if (IETFLanguageTagDictionary.Instance.TryGetValue(ietfIsoCode, out IPluralizer pluralizer)) // Exact culture
                {
                    var plural = pluralizer.Pluralize(noun, customPluralizationDictionary, true);
                    if (!string.IsNullOrWhiteSpace(plural))
                        return plural;
                    pluralizers[i] = pluralizer;
                }
            }
            return null;
        }
    }
}