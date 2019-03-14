using System;
using System.Collections.Concurrent;

namespace Rhyous.StringLibrary.Pluralization
{
    public class IETFLanguageTagDictionary : ConcurrentDictionary<string, IPluralizer>
    {
        #region Singleton

        private static readonly Lazy<IETFLanguageTagDictionary> Lazy = new Lazy<IETFLanguageTagDictionary>(() => new IETFLanguageTagDictionary());

        public static IETFLanguageTagDictionary Instance { get { return Lazy.Value; } }

        internal IETFLanguageTagDictionary()
        {
            Init();
        }

        #endregion

        protected virtual void Init()
        {
            GetOrAdd("en-US", new USEnglishPluralizer());
            GetOrAdd("en", new USEnglishPluralizer());
        }
    }
}
