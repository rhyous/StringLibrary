using System.Collections.Generic;

namespace Rhyous.StringLibrary.Pluralization
{
    public interface IPluralizer
    {
        /// <summary>
        /// A default pluralization dictionary.
        /// </summary>
        IDictionary<string, string> PluralizationDictionary { get; set; }

        /// <summary>
        /// A method that pluralizes an word.
        /// </summary>
        /// <param name="noun">The word to pluralize</param>
        /// <param name="pluralizationDictionary">Optional. If no dictionary is provided, the default dictionary should be used.</param>
        /// <returns>A string, which is the noun pluralized.</returns>
        string Pluralize(string noun, IDictionary<string, string> customPluralizationDiction = null);
    }
}
