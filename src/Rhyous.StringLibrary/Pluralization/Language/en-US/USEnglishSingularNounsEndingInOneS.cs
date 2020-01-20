using System;
using System.Collections;
using System.Collections.Generic;

namespace Rhyous.StringLibrary.Pluralization
{
    /// <summary>
    /// Nouns ending in a single S that are singular. Nouns ending in ss such as boss are excluded.
    /// Also, we are excluding any noun ending in -itis or -osis.
    /// If you see a noun missing, don't hesitate to ask for it to be added: https://github.com/rhyous/StringLibrary
    /// Either: 1. Fork, fix, request a pull request, or 2. Submit an Issue.
    /// </summary>
    public class SingularNounsEndingInOneS : ICollection<string>
    {
        internal ConcurrentHashSet<string> _HashSet = new ConcurrentHashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// The constructor for this object.
        /// </summary>
        public SingularNounsEndingInOneS()
        {
            Init();
        }

        /// <inheritdoc />
        public int Count => _HashSet.Count;

        /// <inheritdoc />
        public bool IsReadOnly => ((ICollection<string>)_HashSet).IsReadOnly;

        /// <inheritdoc />
        public void Add(string item) => _HashSet.Add(item);

        /// <inheritdoc />
        public void Clear() => _HashSet.Clear();

        /// <inheritdoc />
        public bool Contains(string item) => _HashSet.Contains(item);

        /// <inheritdoc />
        public void CopyTo(string[] array, int arrayIndex) => ((ICollection<string>)_HashSet).CopyTo(array, arrayIndex);

        /// <inheritdoc />
        public IEnumerator<string> GetEnumerator() => _HashSet.GetEnumerator();

        /// <inheritdoc />
        public bool Remove(string item) => ((ICollection<string>)_HashSet).Remove(item);

        /// <summary>
        /// A method to initialize this HashSet.
        /// </summary>
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

        IEnumerator IEnumerable.GetEnumerator() => _HashSet.GetEnumerator();
    }
}