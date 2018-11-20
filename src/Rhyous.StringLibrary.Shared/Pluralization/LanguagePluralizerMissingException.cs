using System;
using System.Diagnostics.CodeAnalysis;

namespace Rhyous.StringLibrary.Pluralization
{
    [ExcludeFromCodeCoverage]
    public class LanguagePluralizerMissingException : Exception
    {
        public LanguagePluralizerMissingException(string language) : base($"The pluralizer for language {language} is missing and has not been defined.")
        {
        }
    }
}