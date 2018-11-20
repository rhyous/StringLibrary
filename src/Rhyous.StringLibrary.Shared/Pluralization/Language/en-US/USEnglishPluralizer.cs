using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.StringLibrary.Pluralization
{
    /// <summary>
    /// This should do a pretty good job pluralizing most English words.
    /// Obviously, there is a lot more work to do to reach accuracy: https://en.wikipedia.org/wiki/English_plurals
    /// </summary>
    public class USEnglishPluralizer : IPluralizer
    {
        public List<string> EndingsThatPuralizeWithEs { get; } = new List<string> { "ch", "s", "sh", "x", "z" };
        public List<string> Vowels { get; } = new List<string> { "a", "e", "i", "o", "u" };
        public List<string> Diagraphs = new List<string> { "ch", "ci", "ck", "gh", "ng", "ph", "qu", "rh", "sc", "sh", "ss", "th", "ti", "wh", "wr", "zh" };

        public IDictionary<string, string> PluralizationDictionary
        {
            get { return _PluralizationDictionary ?? (_PluralizationDictionary = new USEnglishPluralizationDictionary()); }
            set { _PluralizationDictionary = value; }
        } private IDictionary<string, string> _PluralizationDictionary;

        public string Pluralize(string noun, IDictionary<string, string> customPluralizationDictionary = null)
        {
            PluralizationDictionary = customPluralizationDictionary;
            if (IsPlural(noun))
                return noun;
            // Supported Capitalization scenarios
            // [X] All uppercase
            // [X] All lowercase
            // [X] First letter uppercase
            // [ ] More than one letter uppercase.  
            bool allUppercase;
            if (PluralizationDictionary.TryGetValue(noun, out string value))
            {
                if (noun.All(c => char.IsUpper(c))) // All upper case
                    return value.ToUpper();
                if (char.IsUpper(noun[0]))  // First letter uppercase
                    return value.CapitalizeFirstLetter();
                return value; // All lowercase
            }
            allUppercase = noun.Length > 1 && noun.All(c => char.IsUpper(c)) 
                        && !(noun.Length == 2 && Diagraphs.Contains(noun, StringComparer.OrdinalIgnoreCase));
            return ApplyStandardPluralizationRules(noun, allUppercase);
        }

        public string ApplyStandardPluralizationRules(string noun, bool capitalize = false)
        {
            string s = "s", es = "es", ies = "ies";
            if (capitalize)
            {
                s = "S"; es = "ES"; ies = "IES";
            }

            if (string.IsNullOrWhiteSpace(noun))
                return noun;

            // Pluralization of letters and number character names themselves: As, Bs, Cs, Ds, a's, b's, c's, d's, 1s, 2s, 3s.
            if (noun.Length == 1)
            {
                return (char.IsUpper(noun, 0) || char.IsDigit(noun, 0)) ? noun + s : noun + "'" + s;
            }

            // Pluralization of diagraphs themselves: CHs, CKs, Ch's, ch's
            if (noun.Length == 2 && Diagraphs.Contains(noun, StringComparer.OrdinalIgnoreCase))
            {
                return noun + (char.IsLower(noun, 1) ? "'" + s : s);
            }

            // Ends with y and character before y is not a vowel.
            if (noun.EndsWith("y", StringComparison.OrdinalIgnoreCase))
            {
                // Second to last letter is not a vowel. Example: fries.
                if ((noun.Length > 1 && !Vowels.Contains(noun[noun.Length - 2].ToString(), StringComparer.OrdinalIgnoreCase))
                // or ends in -quy. Example: soliloquies
                || (noun.EndsWith("quy", StringComparison.OrdinalIgnoreCase)))
                    return noun.Substring(0, noun.Length - 1) + ies; // flies, ties, treaties
                return noun + s; // Example: boys, toys, joys, guys
            }

            // Nouns that end with consonant followed by o should usually end with -es. If not, put them in the dictionary.
            if (noun.EndsWith("o", StringComparison.OrdinalIgnoreCase) && !Vowels.Contains(noun[noun.Length - 2].ToString(), StringComparer.OrdinalIgnoreCase))
                return noun + es;

            // Nouns that end with a character or diagraphs that requires pluralization with -es
            if (EndingsThatPuralizeWithEs.Any(e => noun.EndsWith(e, StringComparison.OrdinalIgnoreCase)))
                return noun + es;

            // The default, just add an s.
            return noun + s;
        }

        public bool IsPlural(string noun)
        {
            // Handles all irregular plurals and plurals that are are the same as their singular
            if (IrregularPlurals.Contains(noun))
                return true;

            if (SingularsEndingInOneS.Contains(noun))
                return false;

            if (SingularSEndings.Any(ss => noun.EndsWith(ss, StringComparison.OrdinalIgnoreCase)))
                return false;
                       
            if (SingularSEndings.Any(s=> noun.EndsWith(s, StringComparison.OrdinalIgnoreCase)))
                return false;

            if (noun.Length == 1)
                return false;
            
            if (noun.EndsWith("s"))
                return true;

            return false;
        }


        public List<string> SingularSEndings { get; } = new List<string> { "ss", "itis", "osis" };

        public HashSet<string> IrregularPlurals => (_IrregularPlurals == null || _IrregularPlurals.Count != PluralizationDictionary.Count) ? BuildHashSet() : _IrregularPlurals;
        private HashSet<string> _IrregularPlurals;

        private HashSet<string> BuildHashSet()
        {
            _IrregularPlurals = new HashSet<string>();
            foreach (var plural in PluralizationDictionary.Values)
            {
                _IrregularPlurals.Add(plural);
            }
            return _IrregularPlurals;
        }

        public HashSet<string> SingularsEndingInOneS
        {
            get { return _SingularsEndingInOneS ?? (_SingularsEndingInOneS = new SingularNounsEndingInOneS()); }
            set { _SingularsEndingInOneS = value; }
        } private HashSet<string> _SingularsEndingInOneS;

    }
}