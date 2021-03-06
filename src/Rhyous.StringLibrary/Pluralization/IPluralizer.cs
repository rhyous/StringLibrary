﻿using System.Collections.Generic;

namespace Rhyous.StringLibrary.Pluralization
{
    /// <summary>
    /// An interface for Pluralizers.
    /// </summary>
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
        /// <param name="customOnly">Will only pluralize if the word is found in either the provided custom dictionary or the default custom dictionary.</param>
        /// <returns>A string, which is the noun pluralized.</returns>
        string Pluralize(string noun, IDictionary<string, string> pluralizationDictionary = null, bool customOnly = false);
    }
}
