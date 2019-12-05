using System;
using System.Collections;
using System.Collections.Generic;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// A class to manage the global static settings for Trim.
    /// </summary>
    public class TrimPropertiesSettings
    {
        /// <summary>
        /// A setting to determine if we trimm by default or not. 
        /// It is true by default.
        /// </summary>        
        public static bool TrimByDefault { get; set; } = true;

        /// <summary>
        /// A list of supported generic trimmable types.
        /// </summary>
        public static List<Type> SupportedGenericTypes = new List<Type> {
            typeof(IList),
            typeof(IDictionary),
            typeof(IList<>),
            typeof(IDictionary<,>)
        };
    }
}
