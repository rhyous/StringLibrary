using System;
using System.Collections.Generic;

namespace Rhyous.StringLibrary.Pluralization
{
    /// <summary>
    /// Nouns ending in a single S that are singular. Nouns ending in ss such as boss are excluded.
    /// Also, we are excluding any noun ending in -itis or -osis.
    /// If you see a noun missing, don't hesitate to ask for it to be added: https://github.com/rhyous/StringLibrary
    /// Either: 1. Fork, fix, request a pull request, or 2. Submit an Issue.
    /// </summary>
    public class SingularNounsEndingInOneS : HashSet<string>
    {
        public SingularNounsEndingInOneS() : base(StringComparer.OrdinalIgnoreCase)
        {
            Init();
        }

        internal void Init()
        {
            Add("alumnus");
            Add("analysis");
            Add("antithesis");
            Add("axis");
            Add("bacillus");
            Add("basis");
            Add("bus");
            Add("corpus");
            Add("cactus");
            Add("crisis");
            Add("discus");
            Add("ellipsis");
            Add("focus");
            Add("fungus");
            Add("gas");
            Add("genus");
            Add("hypothesis");
            Add("iris");
            Add("locus");
            Add("nucleus");
            Add("oasis");
            Add("opus");
            Add("parenthesis");
            Add("radius");
            Add("rhinoceros");
            Add("series");
            Add("species");
            Add("stimulus");
            Add("syllabus");
            Add("synopsis");
            Add("thesis");
        }
    }
}