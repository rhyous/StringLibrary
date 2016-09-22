using System;
using System.Collections;
using System.Collections.Generic;

namespace Rhyous.StringLibrary
{
    public class TrimPropertiesSettings
    {
        public static bool TrimByDefault { get; set; } = true;

        public static List<Type> SupportedGenericTypes = new List<Type> {
            typeof(IList),
            typeof(IDictionary),
            typeof(IList<>),
            typeof(IDictionary<,>)
        };
    }
}
