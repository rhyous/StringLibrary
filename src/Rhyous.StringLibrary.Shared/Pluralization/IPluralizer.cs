using System.Collections.Generic;

namespace Rhyous.StringLibrary.Pluralization
{
    public interface IPluralizer
    {
        IDictionary<string, string> PluralizationDictionary { get; set; }
        string Pluralize(string noun, IDictionary<string, string> customPluralizationDiction = null);
    }
}
