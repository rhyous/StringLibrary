using System;
using System.Diagnostics.CodeAnalysis;

namespace Rhyous.StringLibrary.Pluralization
{
    /// <summary>
    /// An exception class for when a pluralizer is missing for a language.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class LanguagePluralizerMissingException : Exception
    {
        /// <summary>
        /// The exception constructor.
        /// </summary>
        /// <param name="language">The language the pluralize is missing for.</param>
        public LanguagePluralizerMissingException(string language) : base($"The pluralizer for language {language} is missing and has not been defined.")
        {
        }
    }
}