using System;
using System.Collections.Concurrent;

namespace Rhyous.StringLibrary.Pluralization
{
    /// <summary>
    /// A dictionary of Pluralizers for different IETF language tags
    /// </summary>
    public class IETFLanguageTagDictionary : ConcurrentDictionary<string, IPluralizer>
    {
        #region Singleton

        private static readonly Lazy<IETFLanguageTagDictionary> Lazy = new Lazy<IETFLanguageTagDictionary>(() => new IETFLanguageTagDictionary());

        /// <summary>The singleton.</summary>
        public static IETFLanguageTagDictionary Instance { get { return Lazy.Value; } }

        internal IETFLanguageTagDictionary()
        {
            Init();
        }

        #endregion

        /// <summary>
        /// Initialized the default pluralizers.
        /// </summary>
        /// <remarks>only US english exists so far and it is also used for 'en' fallback.</remarks>
        protected virtual void Init()
        {
            GetOrAdd("en-US", new USEnglishPluralizer());
            GetOrAdd("en", new USEnglishPluralizer());
        }
    }
}
